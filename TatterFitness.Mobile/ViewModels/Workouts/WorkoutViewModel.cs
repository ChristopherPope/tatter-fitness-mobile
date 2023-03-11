using AutoMapper;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.ObjectModel;
using TatterFitness.Mobile.Controls.Popups;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;
using TatterFitness.Mobile.Interfaces.Services.ContextMenu;
using TatterFitness.Mobile.Interfaces.Services.SelectorModals;
using TatterFitness.Mobile.Messages;
using TatterFitness.Mobile.Models.Popups;
using TatterFitness.Mobile.NavData;
using TatterFitness.Mobile.Views.History;
using TatterFitness.Mobile.Views.Workouts.WorkoutExercises;
using TatterFitness.Models.Enums;
using TatterFitness.Models.Exercises;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.ViewModels.Workouts
{
    public partial class WorkoutViewModel :
        ViewModelBase,
        IQueryAttributable,
        IRecipient<CompletedSetMetricsChangedMessage>,
        IRecipient<SetCompletedMessage>
    {
        private int routineId;
        private Workout workout;
        private readonly IWorkoutsApiService workoutsApi;
        private readonly IRoutinesApiService routinesApi;
        private readonly IWorkoutExerciseModifiersApiService modsApi;
        private readonly IWorkoutExerciseSetsApiService setsApi;
        private readonly IModsSelectorModal modsSelectorModal;
        private readonly IExercisesSelectorModal exercisesSelectorModal;
        private readonly IWorkoutExerciseContextMenuService contextMenu;
        private readonly IMapper mapper;
        private readonly IWorkoutExercisesApiService workoutExercisesApi;

        [ObservableProperty]
        private TotalEffortViewModel totalEffort;

        [ObservableProperty]
        private ObservableCollection<WorkoutCardViewModel> exerciseVms = new();

        [ObservableProperty]
        private ObservableCollection<WorkoutExerciseSet> sets = new();

        public WorkoutViewModel(ILoggingService logger,
            IWorkoutsApiService workoutsApi,
            IRoutinesApiService routinesApi,
            IWorkoutExercisesApiService workoutExercisesApi,
            IWorkoutExerciseModifiersApiService modsApi,
            IMapper mapper,
            IWorkoutExerciseSetsApiService setsApi,
            IModsSelectorModal modsSelectorModal,
            IExercisesSelectorModal exercisesSelectorModal,
            IWorkoutExerciseContextMenuService contextMenu,
            TotalEffortViewModel totalEffort)
            : base(logger)
        {
            this.workoutsApi = workoutsApi;
            this.routinesApi = routinesApi;
            this.mapper = mapper;
            this.workoutExercisesApi = workoutExercisesApi;
            this.modsApi = modsApi;
            this.setsApi = setsApi;
            this.modsSelectorModal = modsSelectorModal;
            this.exercisesSelectorModal = exercisesSelectorModal;
            this.contextMenu = contextMenu;
            this.totalEffort = totalEffort;

            WeakReferenceMessenger.Default.Register(this as IRecipient<CompletedSetMetricsChangedMessage>);
            WeakReferenceMessenger.Default.Register(this as IRecipient<SetCompletedMessage>);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var navData = NavDataBase.FromNavDataDictionary<WorkoutNavData>(query);
            routineId = navData.RoutineId;
        }

        protected override async Task PerformLoadViewData()
        {
            routineId = 1029;
            var routine = await routinesApi.Read(routineId);
            if (routine == null)
            {
                return;
            }
            workout = await workoutsApi.Create(new Workout { Name = routine.Name });
            Title = workout.Name;

            await PopulateWorkoutExercises(routine.Exercises);
            TotalEffort.ShowTotalEffort(ExerciseVms.SelectMany(vm => vm.WorkoutExercise.Sets));

            await PerformWorkout(ExerciseVms.First());
        }

        public override Task RefreshView()
        {
            TotalEffort.ShowTotalEffort(workout.Exercises.SelectMany(e => e.Sets));

            return Task.CompletedTask;
        }

        private async Task PopulateWorkoutExercises(IEnumerable<RoutineExercise> routineExercises)
        {
            var mostRecentWorkoutExercises = await workoutExercisesApi.ReadMostRecent(routineExercises.Select(e => e.ExerciseId));
            var newExerciseIds = routineExercises.Select(e => e.ExerciseId).Except(mostRecentWorkoutExercises.Select(we => we.ExerciseId));

            var sequence = 1;
            foreach (var routineExercise in routineExercises)
            {
                var mostRecentWorkoutExercise = mostRecentWorkoutExercises.FirstOrDefault(we => we.ExerciseId == routineExercise.ExerciseId);
                var workoutExercise = new WorkoutExercise
                {
                    ExerciseId = routineExercise.ExerciseId,
                    WorkoutId = workout.Id,
                    Sequence = sequence++,
                    ExerciseName = routineExercise.ExerciseName,
                    ExerciseType = routineExercise.ExerciseType,
                    Sets = GetDefaultSets(routineExercise.ExerciseType),

                    Mods = (mostRecentWorkoutExercise == null ? Enumerable.Empty<WorkoutExerciseModifier>().ToList() : GetModsFromWorkoutExercise(mostRecentWorkoutExercise))
                };

                workout.Exercises.Add(workoutExercise);
                var cardVm = new WorkoutCardViewModel(logger, modsSelectorModal, modsApi, mapper, workoutExercise);
                await cardVm.LoadViewData();
                ExerciseVms.Add(cardVm);
            }
        }

        private List<WorkoutExerciseModifier> GetModsFromWorkoutExercise(WorkoutExercise mostRecentExercise)
        {
            List<WorkoutExerciseModifier> mods = mostRecentExercise.Mods;
            foreach (var mod in mods)
            {
                mod.Id = 0;
                mod.WorkoutExerciseId = 0;
            }

            return mods;
        }

        private List<WorkoutExerciseSet> GetSetsFromWorkoutExercise(WorkoutExercise mostRecentExercise, ExerciseTypes exerciseType)
        {
            List<WorkoutExerciseSet> sets = mostRecentExercise.Sets;
            var setNumber = 1;
            foreach (var set in sets)
            {
                set.Id = 0;
                set.WorkoutExerciseId = 0;
                set.SetNumber = setNumber++;
            }

            return sets;
        }

        private List<WorkoutExerciseSet> GetDefaultSets(ExerciseTypes exerciseType)
        {
            var sets = new List<WorkoutExerciseSet>();
            var setNumber = 1;
            switch (exerciseType)
            {
                case ExerciseTypes.Cardio:
                    sets.Add(new WorkoutExerciseSet(setNumber++, exerciseType));
                    break;

                case ExerciseTypes.DurationAndWeight:
                case ExerciseTypes.RepsOnly:
                case ExerciseTypes.RepsAndWeight:
                    sets.Add(new WorkoutExerciseSet(setNumber++, exerciseType));
                    sets.Add(new WorkoutExerciseSet(setNumber++, exerciseType));
                    sets.Add(new WorkoutExerciseSet(setNumber++, exerciseType));
                    break;
            }

            return sets;
        }

        [RelayCommand]
        private async Task ShowContextMenu(WorkoutCardViewModel cardVm)
        {
            try
            {
                var chosenMenuItem = await contextMenu.Show(cardVm);
                switch (chosenMenuItem)
                {
                    case IWorkoutExerciseContextMenuService.RemoveExerciseMenuId:
                        await RemoveExercise(cardVm);
                        break;

                    case IWorkoutExerciseContextMenuService.EditNotesMenuId:
                        await EditNotes(cardVm);
                        break;

                    case IWorkoutExerciseContextMenuService.EditModsMenuId:
                        await cardVm.SelectMods();
                        break;

                    case IWorkoutExerciseContextMenuService.ShowExerciseHistoryMenuId:
                        await ShowExerciseHistory(cardVm);
                        break;
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        private async Task EditNotes(WorkoutCardViewModel cardVm)
        {
            try
            {
                var metadata = new ValueEntryPopupMetadata
                {
                    Prompt = "Enter Notes",
                    Title = cardVm.ExerciseName,
                    Value = cardVm.WorkoutExercise.Notes
                };
                var notesPopup = new NotesEntryPopup(metadata);
                var notes = await Shell.Current.ShowPopupAsync(notesPopup) as string;
                if (notes == null)
                {
                    return;
                }

                cardVm.WorkoutExercise.Notes = notes;
                if (cardVm.WorkoutExercise.Id > 0)
                {
                    await workoutExercisesApi.Update(cardVm.WorkoutExercise);
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        [RelayCommand]
        private async Task AddRemoveExercises()
        {
            try
            {
                var requiredExerciseIds = workout.Exercises
                    .Where(e => e.Sets.Any(s => s.Id > 0))
                    .Select(e => e.ExerciseId);
                await exercisesSelectorModal.ShowModal(ExerciseVms.Select(vm => vm.WorkoutExercise.ExerciseId), requiredExerciseIds, ExercisesModalClosed);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        private async Task ExercisesModalClosed(IEnumerable<Exercise> exercisesToAdd, IEnumerable<Exercise> exercisesToRemove)
        {
            try
            {
                await RemoveExercises(exercisesToRemove.Select(e => e.Id));
                await AddExercises(exercisesToAdd);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        [RelayCommand]
        private async Task RenameWorkout()
        {
            try
            {
                var metadata = new ValueEntryPopupMetadata
                {
                    Title = "Workout Name",
                    Value = workout.Name
                };
                var popup = new ValueEntryPopup(metadata);


                var workoutName = await Shell.Current.ShowPopupAsync(popup) as string;
                if (workoutName == null)
                {
                    return;
                }

                workout.Name = workoutName;
                await workoutsApi.Update(workout);
                Title = workoutName;
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        private async Task RemoveExercise(WorkoutCardViewModel cardVm)
        {
            await RemoveExercises(new List<int> { cardVm.WorkoutExercise.ExerciseId });
        }

        private async Task RemoveExercises(IEnumerable<int> exerciseIds)
        {
            if (!exerciseIds.Any())
            {
                return;
            }

            foreach (var exerciseId in exerciseIds)
            {
                var exerciseVm = ExerciseVms.First(vm => vm.WorkoutExercise.ExerciseId == exerciseId);

                if (exerciseVm.WorkoutExercise.Id > 0)
                {
                    await workoutExercisesApi.Delete(exerciseVm.WorkoutExercise.Id);
                }

                ExerciseVms.Remove(exerciseVm);
            }
        }

        private async Task AddExercises(IEnumerable<Exercise> exercises)
        {
            var routineExercises = new List<RoutineExercise>();
            foreach (var exercise in exercises)
            {
                routineExercises.Add(new RoutineExercise
                {
                    ExerciseId = exercise.Id,
                    ExerciseType = exercise.ExerciseType,
                    ExerciseName = exercise.Name,
                });
            }

            await PopulateWorkoutExercises(routineExercises);
        }

        [RelayCommand]
        private async Task PerformWorkout(WorkoutCardViewModel cardVm)
        {
            try
            {
                var navData = new WorkoutExerciseNavData(cardVm.WorkoutExercise, workout);
                var viewName = cardVm.WorkoutExercise.ExerciseType switch
                {
                    ExerciseTypes.Cardio => nameof(CardioWorkoutExerciseView),
                    ExerciseTypes.RepsAndWeight => nameof(RepsAndWeightWorkoutExerciseView),
                    ExerciseTypes.DurationAndWeight => nameof(DurationAndWeightWorkoutExerciseView),
                    ExerciseTypes.RepsOnly => nameof(RepsOnlyWorkoutExerciseView),
                    _ => string.Empty,
                };

                await Shell.Current.GoToAsync(viewName, true, navData.ToNavDataDictionary());
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        private async Task ShowExerciseHistory(WorkoutCardViewModel cardVm)
        {
            var navData = new ExerciseHistoryNavData(cardVm.WorkoutExercise.ExerciseId);
            await Shell.Current.GoToAsync(nameof(ExerciseHistoryView), true, navData.ToNavDataDictionary());
        }

        public void Receive(CompletedSetMetricsChangedMessage message)
        {
            MainThread.BeginInvokeOnMainThread(() => TotalEffort.ShowTotalEffort(workout.Exercises.SelectMany(e => e.Sets)));
        }

        public void Receive(SetCompletedMessage message)
        {
            MainThread.BeginInvokeOnMainThread(() => TotalEffort.ShowTotalEffort(workout.Exercises.SelectMany(e => e.Sets)));
        }
    }
}
