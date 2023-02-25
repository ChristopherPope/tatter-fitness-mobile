using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Interfaces.Services.API;

namespace TatterFitness.App.ViewModels.History.EventCalendar
{
    public partial class WorkoutEventViewModel : ViewModelBase
    {
        private readonly IHistoriesApiService historyApi;
        private bool isLoaded = false;

        [ObservableProperty]
        private DateTime minDateTime = DateTime.Now;

        [ObservableProperty]
        private DateTime maxDateTime = DateTime.Now;

        [ObservableProperty]
        private DateTime displayDate;

        [ObservableProperty]
        public ObservableCollection<SchedulerAppointment> workouts = new();

        public WorkoutEventViewModel(ILoggingService logger, IHistoriesApiService historyApi)
            : base(logger)
        {
            this.historyApi = historyApi;
            Title = "Workout History";
            DisplayDate = DateTime.Now;
        }

        protected override async Task PerformLoadViewData()
        {
            IsBusy = true;
            var allEventsInterval = await historyApi.ReadFirstAndLastWorkoutDates();
            MinDateTime = allEventsInterval.InclusiveFrom.Date;
            IsBusy = false;
            isLoaded = true;
            MaxDateTime = allEventsInterval.InclusiveTo.Date;
        }

        //protected override async Task ProcessDayClicked(int eventId)
        //{
        //    var navData = new NavData.WorkoutSnapshotNavData(eventId);
        //    await Shell.Current.GoToAsync(nameof(WorkoutSnapshotView), true, navData.ToNavDataDictionary());
        //}

        [RelayCommand]
        private async void LoadWorkouts(object obj)
        {
            if (obj is not SchedulerQueryAppointmentsEventArgs eventArgs)
            {
                return;
            }

            IsBusy = true;
            var dates = eventArgs.VisibleDates.OrderBy(d => d).ToList();
            var workouts = await historyApi.ReadWorkoutEvents(dates.First(), dates.Last());
            var workoutEvents = new ObservableCollection<SchedulerAppointment>();
            foreach (var workout in workouts)
            {
                var ev = new SchedulerAppointment
                {
                    IsAllDay = true,
                    StartTime = workout.EventDate,
                    EndTime = workout.EventDate.AddHours(1),
                    Subject = "Workout",
                    Background = new SolidColorBrush(Color.FromArgb("#FFFC571D"))
                };

                workoutEvents.Add(ev);
            }

            Workouts = workoutEvents;
            IsBusy = false;
        }
    }
}
