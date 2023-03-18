using AutoMapper;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TatterFitness.Mobile.Controls.Popups;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;
using TatterFitness.Mobile.Interfaces.Services.SelectorModals;
using TatterFitness.Mobile.Models;
using TatterFitness.Mobile.Models.Popups;
using TatterFitness.Mobile.NavData;
using TatterFitness.Mobile.Utils;
using TatterFitness.Mobile.Views.History;
using TatterFitness.Models.Enums;
using TatterFitness.Models.Exercises;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.ViewModels.Workouts.WorkoutExercises
{
    public abstract partial class BaseWorkoutExerciseViewModel<T> :
        ViewModelBase,
        IQueryAttributable
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

        private Workout workout = null;
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
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (workout != null)
            {
                return;
            }

            var navData = NavDataBase.FromNavDataDictionary<WorkoutExerciseNavData>(query);
            workout = navData.Workout;
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
            TotalEffort.ShowTotalEffort(workout.Exercises.SelectMany(we => we.Sets));

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
                if (await Shell.Current.ShowPopupAsync(exercise531Popup) is not FTOResults ftoResults)
                {
                    return;
                }

                IsBusy = true;

                var ftoNotes = new FTONotes(WorkoutExercise);
                ftoNotes.AddFTONotes(ftoResults.TrainingMax, ftoResults.WeekNumber);

                WorkoutExercise.FtoTrainingMax = ftoResults.TrainingMax;
                WorkoutExercise.FtoWeekNumber = ftoResults.WeekNumber;
                WorkoutExercise.Sets = ftoResults.ExerciseSets;
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
                var lastVm = SetVms.LastOrDefault();
                WorkoutExerciseSet newSet;
                if (lastVm == null)
                {
                    newSet = new WorkoutExerciseSet(SetVms.Count + 1, WorkoutExercise.ExerciseType);
                }
                else
                {
                    newSet = mapper.Map<WorkoutExerciseSet>(lastVm.Set);
                    newSet.Id = 0;
                    newSet.SetNumber = SetVms.Count + 1;
                }

                newSet.WorkoutExerciseId = WorkoutExercise.Id;
                WorkoutExercise.Sets.Add(newSet);

                SetVms.Add(CreateSetVm(WorkoutExercise.ExerciseId, newSet, WorkoutExercise.Sets.Count));
                RefreshSetVms();
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        private void RefreshSetVms()
        {
            var setNumber = 1;
            SetVms
                .ToList()
                .ForEach(vm =>
                {
                    vm.Set.SetNumber = setNumber++;
                    vm.Refresh(WorkoutExercise.Sets.Count);
                });
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

                TotalEffort.ShowTotalEffort(workout.Exercises.SelectMany(we => we.Sets));
                RefreshSetVms();
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

                TotalEffort.ShowTotalEffort(workout.Exercises.SelectMany(we => we.Sets));
                SetButtonAvailability();
                RefreshSetVms();
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

        [RelayCommand]
        private async Task MetricUpdated(int setId)
        {
            try
            {
                var setVm = SetVms.FirstOrDefault(vm => vm.Set.Id == setId);
                if (setVm == null || setId == 0)
                {
                    return;
                }
                var set = setVm.Set;

                var exerciseType = set.ExerciseType;
                var updatedSet = Task.Run(async () =>
                {
                    return await setsApi.Update(set);
                }).Result;

                mapper.Map(updatedSet, set);
                set.ExerciseType = exerciseType;
                TotalEffort.ShowTotalEffort(workout.Exercises.SelectMany(we => we.Sets));
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }
    }
}
