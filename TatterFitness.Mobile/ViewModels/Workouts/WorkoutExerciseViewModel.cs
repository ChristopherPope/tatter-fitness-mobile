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
using TatterFitness.Mobile.ViewModels;
using TatterFitness.Models.Enums;
using TatterFitness.Models.Exercises;
using TatterFitness.Models.Workouts;

namespace TatterFitness.App.ViewModels.Workouts
{
    public partial class WorkoutExerciseViewModel : ViewModelBase, IQueryAttributable, IRecipient<CompletedSetMetricsChangedMessage>
    {
        private readonly IWorkoutExercisesApiService workoutExercisesApi;
        private readonly IWorkoutExerciseModifiersApiService modsApi;
        private readonly IWorkoutExerciseSetsApiService setsApi;
        private readonly IModsSelectorModal modsSelectorModal;
        private readonly IMapper mapper;
        private int currentPosition;

        [ObservableProperty]
        private ObservableCollection<SetViewModel> setVms = new();

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

        private WorkoutExerciseSet CurrentSet => WorkoutExercise.Sets[currentPosition];
        private SetViewModel CurrentSetVm => SetVms[currentPosition];

        public WorkoutExerciseViewModel(ILoggingService logger,
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

        protected override Task PerformLoadViewData()
        {
            Title = WorkoutExercise.ExerciseName;
            FormModNames();
            CreateSetVms();
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
                    ExerciseId = workoutExercise.ExerciseId,
                    ExerciseName = workoutExercise.ExerciseName
                };
                var exercise531Popup = new Exercise531Popup(metadata);
                var sets = await Shell.Current.ShowPopupAsync(exercise531Popup) as List<WorkoutExerciseSet>;
                if (sets == null)
                {
                    return;
                }

                IsBusy = true;
                workoutExercise.Sets = sets;
                CreateSetVms();
                IsBusy = false;
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        private void CreateSetVms()
        {
            SetVms.Clear();
            for (var i = 0; i < WorkoutExercise.Sets.Count; i++)
            {
                var set = WorkoutExercise.Sets[i];
                SetVms.Add(new SetViewModel(set, WorkoutExercise.ExerciseType, WorkoutExercise.Sets.Count));
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
                    newSet.SetNumber++;
                }

                newSet.WorkoutExerciseId = WorkoutExercise.Id;
                WorkoutExercise.Sets.Add(newSet);
                SetVms.Add(new SetViewModel(newSet, WorkoutExercise.ExerciseType, WorkoutExercise.Sets.Count));

                foreach (var setVm in SetVms)
                {
                    setVm.TotalSets = WorkoutExercise.Sets.Count;
                }

                WeakReferenceMessenger.Default.Send(new SetAddedMessage(newSet));
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
                CurrentSetVm.IsCompleted = true;

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
                var setVm = CurrentSetVm;
                SetVms.Remove(setVm);
                if (setVm.IsCompleted)
                {
                    await setsApi.Delete(setVm.Set.Id);
                }

                WorkoutExercise.Sets.Remove(setVm.Set);

                RenumberSets();
                totalEffort.ShowTotalEffort(WorkoutExercise.Sets);
                SetButtonAvailability();

                WeakReferenceMessenger.Default.Send(new SetDeletedMessage(setVm.Set));
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        private void RenumberSets()
        {
            var setNumber = 1;
            foreach (var setVm in SetVms)
            {
                setVm.SetNumber = setNumber++;
                setVm.TotalSets = WorkoutExercise.Sets.Count;
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
    }
}
