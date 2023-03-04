using CommunityToolkit.Mvvm.ComponentModel;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.ViewModels.Home
{
    public partial class HomeCardViewModel : ViewModelBase
    {
        private readonly IWorkoutsApiService workoutsApi;

        [ObservableProperty]
        private string dayOfWeek;

        [ObservableProperty]
        private string workoutName;

        public Workout Workout { get; private set; }

        public HomeCardViewModel(ILoggingService logger, IWorkoutsApiService workoutsApi, string dayOfWeek, Workout workout = null)
            : base(logger)
        {
            this.workoutsApi = workoutsApi;
            Workout = workout; 
            WorkoutName = workout?.Name ?? String.Empty;
            DayOfWeek = dayOfWeek;
        }

        protected override Task PerformLoadViewData()
        {
            return Task.CompletedTask;
        }
    }
}
