using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;
using TatterFitness.Mobile.Interfaces.Services.ContextMenu;
using TatterFitness.Mobile.NavData;
using TatterFitness.Mobile.Views.History;
using TatterFitness.Mobile.ViewModels;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.ViewModels.WorkoutSnapshot
{
    public partial class WorkoutSnapshotViewModel : ViewModelBase, IQueryAttributable
    {
        private int workoutId;
        private readonly IWorkoutsApiService workoutsApi;
        private readonly IWorkoutSnapshotContextMenuService contextMenu;

        [ObservableProperty]
        private string workoutName;

        [ObservableProperty]
        private string workoutDate;

        [ObservableProperty]
        private IEnumerable<WorkoutExercise> workoutExercises;

        [ObservableProperty]
        private ObservableCollection<WorkoutSnapshotCardViewModel> cardVms = new();

        [ObservableProperty]
        private ObservableCollection<WorkoutExerciseSet> sets = new();

        public TotalEffortViewModel TotalEffort { get; private set; }


        public WorkoutSnapshotViewModel(ILoggingService logger, IWorkoutsApiService workoutsApi, IWorkoutSnapshotContextMenuService contextMenu, TotalEffortViewModel totalEffort) : base(logger)
        {
            this.workoutsApi = workoutsApi;
            this.contextMenu = contextMenu;
            TotalEffort = totalEffort;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var navData = NavDataBase.FromNavDataDictionary<WorkoutSnapshotNavData>(query);

            workoutId = navData.WorkoutId;
        }

        protected override async Task PerformLoadViewData()
        {
            var workout = await workoutsApi.Read(workoutId);
            WorkoutName = string.IsNullOrEmpty(workout.Name) ? "Workout" : workout.Name;
            WorkoutDate = workout.Date.ToString("ddd MM/dd/yy");

            foreach (var workoutExercise in workout.Exercises)
            {
                var vm = new WorkoutSnapshotCardViewModel(logger, workoutExercise);
                cardVms.Add(vm);
                await vm.LoadViewData();
            }

            WorkoutExercises = workout.Exercises;

            var sets = WorkoutExercises.SelectMany(we => we.Sets);
            foreach (var set in sets)
            {
                Sets.Add(set);
            }

            TotalEffort.ShowTotalEffort(sets);
        }

        [RelayCommand]
        private async Task ShowContextMenu(WorkoutSnapshotCardViewModel cardVm)
        {
            try
            {
                var selection = await contextMenu.Show(cardVm);
                switch (selection)
                {
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

        private async Task ShowExerciseHistory(WorkoutSnapshotCardViewModel cardVm)
        {
            var navData = new ExerciseHistoryNavData(cardVm.WorkoutExercise.ExerciseId);
            await Shell.Current.GoToAsync(nameof(ExerciseHistoryView), true, navData.ToNavDataDictionary());
        }
    }
}
