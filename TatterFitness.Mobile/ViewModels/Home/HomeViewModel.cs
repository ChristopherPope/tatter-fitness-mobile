using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;
using TatterFitness.Mobile.Views.Workouts;

namespace TatterFitness.Mobile.ViewModels.Home
{
    public partial class HomeViewModel : ViewModelBase
    {
        private readonly IHistoriesApiService historiesApi;
        private readonly IWorkoutsApiService workoutsApi;

        [ObservableProperty]
        private ObservableCollection<HomeCardViewModel> homeCardVms = new();

        public HomeViewModel(ILoggingService logger, IHistoriesApiService historiesApi, IWorkoutsApiService workoutsApi)
            : base(logger)
        {
            this.historiesApi = historiesApi;
            this.workoutsApi = workoutsApi;
        }

        protected override async Task PerformLoadViewData()
        {
            if (HomeCardVms.Any())
            {
                return;
            }

            Title = "Home";
            var today = DateTime.Now.Date;
            var start = today.AddDays(-6);

            var workouts = (await historiesApi.ReadWorkouts(start, today)).ToList();
            var day = today.Date;
            while (day >= start)
            {
                var workout = workouts.Where(wo => wo.Date.Date == day).FirstOrDefault();
                HomeCardViewModel cardVm = new(logger, workoutsApi, day.ToString("ddd"), workout);
                await cardVm.LoadViewData();
                HomeCardVms.Add(cardVm);
                day = day.AddDays(-1);
            }

            var navData = new NavData.WorkoutSnapshotNavData(Convert.ToInt32(18817));
            await Shell.Current.GoToAsync(nameof(WorkoutSnapshotView), true, navData.ToNavDataDictionary());
        }

        [RelayCommand]
        private async Task ViewWorkout(HomeCardViewModel cardVm)
        {
            try
            {
                if (cardVm.Workout == null)
                {
                    return;
                }

                var navData = new NavData.WorkoutSnapshotNavData(cardVm.Workout.Id);
                await Shell.Current.GoToAsync(nameof(WorkoutSnapshotView), true, navData.ToNavDataDictionary());
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }
    }
}
