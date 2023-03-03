using AutoMapper;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using TatterFitness.App.Controls.Popups;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Interfaces.Services.API;
using TatterFitness.App.Interfaces.Services.SelectorModals;
using TatterFitness.App.Models.Popups;
using TatterFitness.App.NavData;
using TatterFitness.App.Views.History;
using TatterFitness.Mobile.Messages;
using TatterFitness.Mobile.Messages.MessageArgs;
using TatterFitness.Mobile.ViewModels;
using TatterFitness.Models.Enums;
using TatterFitness.Models.Exercises;
using TatterFitness.Models.Workouts;

namespace TatterFitness.App.ViewModels.Workouts.WorkoutExercises
{
    public abstract partial class BaseWorkoutExerciseViewModel<T> :
        ViewModelBase,
        IQueryAttributable,
        IRecipient<CompletedSetMetricsChangedMessage>
        where T : BaseSetViewModel
    {
        private readonly IWorkoutExercisesApiService workoutExercisesApi;
        private readonly IWorkoutExerciseModifiersApiService modsApi;
        private readonly IWorkoutExerciseSetsApiService setsApi;
        private readonly IModsSelectorModal modsSelectorModal;
        private readonly IMapper mapper;
        private int currentPosition;

        [ObservableProperty]
        private string modNames;

        [ObservableProperty]
        private bool doShow531Button;

        [ObservableProperty]
        private bool doShowCompleteSetButton = true;

        [ObservableProperty]
        private int position;

        [ObservableProperty]
        private string exerciseName;

        [ObservableProperty]
        private WorkoutExercise workoutExercise;

        [ObservableProperty]
        private TotalEffortViewModel totalEffort;

        [ObservableProperty]
        private ObservableCollection<T> setVms = new();

        private WorkoutExerciseSet CurrentSet => WorkoutExercise.Sets[currentPosition];
        private T CurrentSetVm => SetVms[currentPosition];

        public BaseWorkoutExerciseViewModel(ILoggingService logger,
            IMapper mapper,
            IModsSelectorModal modsSelectorModal,
            IWorkoutExercisesApiService workoutExercisesApi,
            IWorkoutExerciseModifiersApiService modsApi,
            IWorkoutExerciseSetsApiService setsApi,
            TotalEffortViewModel totalEffort)
            : base(logger)
        {
            this.workoutExercisesApi = workoutExercisesApi;
            this.modsApi = modsApi;
            this.setsApi = setsApi;
            this.mapper = mapper;
            this.modsSelectorModal = modsSelectorModal;
            this.totalEffort = totalEffort;
            CreateMonkeyCollection();

            WeakReferenceMessenger.Default.Register(this);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (WorkoutExercise != null)
            {
                return;
            }

            var navData = NavDataBase.FromNavDataDictionary<WorkoutExerciseNavData>(query);

            WorkoutExercise = navData.WorkoutExercise;
        }

        abstract protected T CreateSetVm(int exerciseId, WorkoutExerciseSet set, int totalSets);

        protected override Task PerformLoadViewData()
        {
            Title = WorkoutExercise.ExerciseName;
            FormModNames();
            foreach (var set in WorkoutExercise.Sets)
            {
                SetVms.Add(CreateSetVm(WorkoutExercise.ExerciseId, set, WorkoutExercise.Sets.Count));
            }
            SetButtonAvailability();
            totalEffort.ShowTotalEffort(WorkoutExercise.Sets);

            return Task.CompletedTask;
        }

        private void FormModNames()
        {
            ExerciseName = WorkoutExercise.ExerciseName;
            WorkoutExercise.Mods
                .Sort((mod1, mod2) => mod1.ModifierSequence.CompareTo(mod2.ModifierSequence));

            var modNames = WorkoutExercise.Mods
                .Select(e => e.ModifierName).ToArray();
            ModNames = string.Join(", ", modNames);
        }

        [RelayCommand]
        private async Task Calculate531()
        {
            try
            {
                var metadata = new Exercise531PopupMetadata
                {
                    ExerciseId = WorkoutExercise.ExerciseId,
                    ExerciseName = WorkoutExercise.ExerciseName
                };
                var exercise531Popup = new Exercise531Popup(metadata);
                if (await Shell.Current.ShowPopupAsync(exercise531Popup) is not List<WorkoutExerciseSet> sets)
                {
                    return;
                }

                IsBusy = true;
                WorkoutExercise.Sets = sets;
                SetVms.Clear();
                foreach (var set in WorkoutExercise.Sets)
                {
                    SetVms.Add(CreateSetVm(WorkoutExercise.ExerciseId, set, WorkoutExercise.Sets.Count));
                }

                IsBusy = false;
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        [RelayCommand]
        private async Task EditNotes()
        {
            try
            {
                var metadata = new ValueEntryPopupMetadata
                {
                    Prompt = "Enter Notes",
                    Title = WorkoutExercise.ExerciseName,
                    Value = WorkoutExercise.Notes
                };
                var notesPopup = new NotesEntryPopup(metadata);
                if (await Shell.Current.ShowPopupAsync(notesPopup) is not string notes)
                {
                    return;
                }

                WorkoutExercise.Notes = notes;
                if (WorkoutExercise.Id > 0)
                {
                    await workoutExercisesApi.Update(WorkoutExercise);
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        [RelayCommand]
        private async Task AddSet()
        {
            try
            {
                var lastVm = setVms.LastOrDefault();
                WorkoutExerciseSet newSet;
                if (lastVm == null)
                {
                    newSet = new WorkoutExerciseSet(setVms.Count + 1, WorkoutExercise.ExerciseType);
                }
                else
                {
                    newSet = mapper.Map<WorkoutExerciseSet>(lastVm.Set);
                    newSet.Id = 0;
                    newSet.SetNumber = setVms.Count + 1;
                }

                newSet.WorkoutExerciseId = WorkoutExercise.Id;
                WorkoutExercise.Sets.Add(newSet);

                var args = new SetCountChangedArgs(WorkoutExercise.ExerciseId, WorkoutExercise.Sets.Count);
                WeakReferenceMessenger.Default.Send(new SetCountChangedMessage(args));

                SetVms.Add(CreateSetVm(WorkoutExercise.ExerciseId, newSet, WorkoutExercise.Sets.Count));
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        [RelayCommand]
        private async Task SelectMods()
        {
            try
            {
                await modsSelectorModal.ShowModal(WorkoutExercise.Mods.Select(m => m.ExerciseModifierId), OnSelectModsModalClosed);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        private async Task OnSelectModsModalClosed(IEnumerable<ExerciseModifier> modsToAdd, IEnumerable<ExerciseModifier> modsToRemove)
        {
            try
            {
                foreach (var weMod in mapper.Map<IEnumerable<ExerciseModifier>, IEnumerable<WorkoutExerciseModifier>>(modsToAdd).ToList())
                {
                    weMod.WorkoutExerciseId = WorkoutExercise.Id;

                    if (WorkoutExercise.Id > 0)
                    {
                        var newWeMod = await modsApi.Create(weMod);
                        mapper.Map(newWeMod, weMod);
                    }

                    WorkoutExercise.Mods.Add(weMod);
                }

                foreach (var mod in modsToRemove)
                {
                    var idx = WorkoutExercise.Mods.FindIndex(m => m.ExerciseModifierId == mod.Id);
                    var weMod = WorkoutExercise.Mods[idx];
                    if (WorkoutExercise.Id > 0)
                    {
                        await modsApi.Delete(weMod.Id);
                    }

                    WorkoutExercise.Mods.RemoveAt(idx);
                }

                FormModNames();
                WeakReferenceMessenger.Default.Send(new ExerciseModsChangedMessage(WorkoutExercise));
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        [RelayCommand]
        private async Task CompleteSet()
        {
            try
            {
                if (WorkoutExercise.Id == 0)
                {
                    await CreateWorkout();
                }

                var updatedSet = await setsApi.Create(CurrentSet);
                var exerciseType = CurrentSet.ExerciseType;
                mapper.Map(updatedSet, CurrentSet);
                CurrentSet.ExerciseType = exerciseType;

                WeakReferenceMessenger.Default.Send(new SetCompletedMessage(updatedSet));
                totalEffort.ShowTotalEffort(WorkoutExercise.Sets);
                SetButtonAvailability();

                if (WorkoutExercise.Sets.Count == 1)
                {
                    return;
                }

                if (Position == WorkoutExercise.Sets.Count - 1)
                {
                    Position = 0;
                }
                else
                {
                    Position++;
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        partial void OnPositionChanged(int value)
        {
            logger.Info($"OnPositionChanged({value})");
        }

        partial void OnPositionChanging(int value)
        {
            logger.Info($"OnPositionChanging({value})");
        }

        private async Task CreateWorkout()
        {
            WorkoutExercise.Sets.Clear();
            var newWorkoutExercise = await workoutExercisesApi.Create(WorkoutExercise);
            mapper.Map(newWorkoutExercise, WorkoutExercise);

            WorkoutExercise.Sets.AddRange(SetVms.Select(vm => vm.Set).OrderBy(s => s.SetNumber));
            WorkoutExercise.Sets.ForEach(set => set.WorkoutExerciseId = WorkoutExercise.Id);
        }

        [RelayCommand]
        private async Task ViewHistory()
        {
            try
            {
                var navData = new ExerciseHistoryNavData(WorkoutExercise.ExerciseId);
                await Shell.Current.GoToAsync(nameof(ExerciseHistoryView), true, navData.ToNavDataDictionary());
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        [RelayCommand]
        private async Task PositionChanged(int position)
        {
            try
            {
                logger.Info($"PositionChanged({position})");
                currentPosition = position;
                SetButtonAvailability();
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        [RelayCommand]
        private async Task DeleteSet()
        {
            try
            {
                var setVmToDelete = CurrentSetVm;
                SetVms.Remove(setVmToDelete);
                if (setVmToDelete.IsCompleted)
                {
                    await setsApi.Delete(setVmToDelete.Set.Id);
                }

                WorkoutExercise.Sets.Remove(setVmToDelete.Set);

                totalEffort.ShowTotalEffort(WorkoutExercise.Sets);
                SetButtonAvailability();

                WeakReferenceMessenger.Default.Send(
                    new SetDeletedMessage(
                        new SetDeletedArgs(WorkoutExercise.ExerciseId, setVmToDelete.SetNumber)));

                WeakReferenceMessenger.Default.Send(
                    new SetCountChangedMessage(
                        new SetCountChangedArgs(WorkoutExercise.ExerciseId, WorkoutExercise.Sets.Count)));
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        private void SetButtonAvailability()
        {
            DoShow531Button = WorkoutExercise.ExerciseType == ExerciseTypes.RepsAndWeight && !SetVms.Any(vm => vm.IsCompleted);

            DoShowCompleteSetButton = (SetVms.Any() &&
                (!CurrentSetVm.IsCompleted) &&
                (currentPosition == 0 || SetVms[currentPosition - 1].IsCompleted));
        }

        public void Receive(CompletedSetMetricsChangedMessage message)
        {
            var set = message.Value;
            if (set.Id < 1)
            {
                return;
            }

            var exerciseType = set.ExerciseType;
            var asyncResult = Task.Run(async () =>
            {
                return await setsApi.Update(set);
            });

            var updatedSet = asyncResult.Result;
            mapper.Map(updatedSet, set);
            set.ExerciseType = exerciseType;

            totalEffort.ShowTotalEffort(WorkoutExercise.Sets);
        }


        public IList<Monkey> Monkeys { get; private set; }

        void CreateMonkeyCollection()
        {
            Monkeys = new List<Monkey>();

            Monkeys.Add(new Monkey
            {
                Name = "Baboon",
                Location = "Africa & Asia",
                Details = "Baboons are African and Arabian Old World monkeys belonging to the genus Papio, part of the subfamily Cercopithecinae.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/fc/Papio_anubis_%28Serengeti%2C_2009%29.jpg/200px-Papio_anubis_%28Serengeti%2C_2009%29.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Capuchin Monkey",
                Location = "Central & South America",
                Details = "The capuchin monkeys are New World monkeys of the subfamily Cebinae. Prior to 2011, the subfamily contained only a single genus, Cebus.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Capuchin_Costa_Rica.jpg/200px-Capuchin_Costa_Rica.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Blue Monkey",
                Location = "Central and East Africa",
                Details = "The blue monkey or diademed monkey is a species of Old World monkey native to Central and East Africa, ranging from the upper Congo River basin east to the East African Rift and south to northern Angola and Zambia",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/83/BlueMonkey.jpg/220px-BlueMonkey.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Squirrel Monkey",
                Location = "Central & South America",
                Details = "The squirrel monkeys are the New World monkeys of the genus Saimiri. They are the only genus in the subfamily Saimirinae. The name of the genus Saimiri is of Tupi origin, and was also used as an English name by early researchers.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/20/Saimiri_sciureus-1_Luc_Viatour.jpg/220px-Saimiri_sciureus-1_Luc_Viatour.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Golden Lion Tamarin",
                Location = "Brazil",
                Details = "The golden lion tamarin also known as the golden marmoset, is a small New World monkey of the family Callitrichidae.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/87/Golden_lion_tamarin_portrait3.jpg/220px-Golden_lion_tamarin_portrait3.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Howler Monkey",
                Location = "South America",
                Details = "Howler monkeys are among the largest of the New World monkeys. Fifteen species are currently recognised. Previously classified in the family Cebidae, they are now placed in the family Atelidae.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0d/Alouatta_guariba.jpg/200px-Alouatta_guariba.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Japanese Macaque",
                Location = "Japan",
                Details = "The Japanese macaque, is a terrestrial Old World monkey species native to Japan. They are also sometimes known as the snow monkey because they live in areas where snow covers the ground for months each",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c1/Macaca_fuscata_fuscata1.jpg/220px-Macaca_fuscata_fuscata1.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Mandrill",
                Location = "Southern Cameroon, Gabon, Equatorial Guinea, and Congo",
                Details = "The mandrill is a primate of the Old World monkey family, closely related to the baboons and even more closely to the drill. It is found in southern Cameroon, Gabon, Equatorial Guinea, and Congo.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/75/Mandrill_at_san_francisco_zoo.jpg/220px-Mandrill_at_san_francisco_zoo.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Proboscis Monkey",
                Location = "Borneo",
                Details = "The proboscis monkey or long-nosed monkey, known as the bekantan in Malay, is a reddish-brown arboreal Old World monkey that is endemic to the south-east Asian island of Borneo.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e5/Proboscis_Monkey_in_Borneo.jpg/250px-Proboscis_Monkey_in_Borneo.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Red-shanked Douc",
                Location = "Vietnam, Laos",
                Details = "The red-shanked douc is a species of Old World monkey, among the most colourful of all primates. This monkey is sometimes called the \"costumed ape\" for its extravagant appearance. From its knees to its ankles it sports maroon-red \"stockings\", and it appears to wear white forearm length gloves. Its attire is finished with black hands and feet. The golden face is framed by a white ruff, which is considerably fluffier in males. The eyelids are a soft powder blue. The tail is white with a triangle of white hair at the base. Males of all ages have a white spot on both sides of the corners of the rump patch, and red and white genitals.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9f/Portrait_of_a_Douc.jpg/159px-Portrait_of_a_Douc.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Gray-shanked Douc",
                Location = "Vietnam",
                Details = "The gray-shanked douc langur is a douc species native to the Vietnamese provinces of Quảng Nam, Quảng Ngãi, Bình Định, Kon Tum, and Gia Lai. The total population is estimated at 550 to 700 individuals. In 2016, Dr Benjamin Rawson, Country Director of Fauna & Flora International - Vietnam Programme, announced a discovery of an additional population of more than 500 individuals found in Central Vietnam, bringing the total population up to approximately 1000 individuals.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/0b/Cuc.Phuong.Primate.Rehab.center.jpg/320px-Cuc.Phuong.Primate.Rehab.center.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Golden Snub-nosed Monkey",
                Location = "China",
                Details = "The golden snub-nosed monkey is an Old World monkey in the Colobinae subfamily. It is endemic to a small area in temperate, mountainous forests of central and Southwest China. They inhabit these mountainous forests of Southwestern China at elevations of 1,500-3,400 m above sea level. The Chinese name is Sichuan golden hair monkey. It is also widely referred to as the Sichuan snub-nosed monkey. Of the three species of snub-nosed monkeys in China, the golden snub-nosed monkey is the most widely distributed throughout China.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c8/Golden_Snub-nosed_Monkeys%2C_Qinling_Mountains_-_China.jpg/165px-Golden_Snub-nosed_Monkeys%2C_Qinling_Mountains_-_China.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Black Snub-nosed Monkey",
                Location = "China",
                Details = "The black snub-nosed monkey, also known as the Yunnan snub-nosed monkey, is an endangered species of primate in the family Cercopithecidae. It is endemic to China, where it is known to the locals as the Yunnan golden hair monkey and the black golden hair monkey. It is threatened by habitat loss. It was named after Bishop Félix Biet.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/59/RhinopitecusBieti.jpg/320px-RhinopitecusBieti.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Tonkin Snub-nosed Monkey",
                Location = "Vietnam",
                Details = "The Tonkin snub-nosed monkey or Dollman's snub-nosed monkey is a slender-bodied arboreal Old World monkey, endemic to northern Vietnam. It is a black and white monkey with a pink nose and lips and blue patches round the eyes. It is found at altitudes of 200 to 1,200 m (700 to 3,900 ft) on fragmentary patches of forest on craggy limestone areas. First described in 1912, the monkey was rediscovered in 1990 but is exceedingly rare. In 2008, fewer than 250 individuals were thought to exist, and the species was the subject of intense conservation effort. The main threats faced by these monkeys is habitat loss and hunting, and the International Union for Conservation of Nature has rated the species as \"critically endangered\".",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/9/9c/Tonkin_snub-nosed_monkeys_%28Rhinopithecus_avunculus%29.jpg/320px-Tonkin_snub-nosed_monkeys_%28Rhinopithecus_avunculus%29.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Thomas's Langur",
                Location = "Indonesia",
                Details = "Thomas's langur is a species of primate in the family Cercopithecidae. It is endemic to North Sumatra, Indonesia. Its natural habitat is subtropical or tropical dry forests. It is threatened by habitat loss. Its native names are reungkah in Acehnese and kedih in Alas.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/3/31/Thomas%27s_langur_Presbytis_thomasi.jpg/142px-Thomas%27s_langur_Presbytis_thomasi.jpg"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Purple-faced Langur",
                Location = "Sri Lanka",
                Details = "The purple-faced langur, also known as the purple-faced leaf monkey, is a species of Old World monkey that is endemic to Sri Lanka. The animal is a long-tailed arboreal species, identified by a mostly brown appearance, dark face (with paler lower face) and a very shy nature. The species was once highly prevalent, found in suburban Colombo and the \"wet zone\" villages (areas with high temperatures and high humidity throughout the year, whilst rain deluges occur during the monsoon seasons), but rapid urbanization has led to a significant decrease in the population level of the monkeys.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/02/Semnopithèque_blanchâtre_mâle.JPG/192px-Semnopithèque_blanchâtre_mâle.JPG"
            });

            Monkeys.Add(new Monkey
            {
                Name = "Gelada",
                Location = "Ethiopia",
                Details = "The gelada, sometimes called the bleeding-heart monkey or the gelada baboon, is a species of Old World monkey found only in the Ethiopian Highlands, with large populations in the Semien Mountains. Theropithecus is derived from the Greek root words for \"beast-ape.\" Like its close relatives the baboons, it is largely terrestrial, spending much of its time foraging in grasslands.",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/1/13/Gelada-Pavian.jpg/320px-Gelada-Pavian.jpg"
            });
        }

    }

    public class Monkey
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Details { get; set; }
        public string ImageUrl { get; set; }
    }

}
