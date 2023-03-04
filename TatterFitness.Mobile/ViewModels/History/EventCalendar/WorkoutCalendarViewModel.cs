using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;
using TatterFitness.Mobile.Views.Workouts;

namespace TatterFitness.Mobile.ViewModels.History.EventCalendar
{
    public partial class WorkoutCalendarViewModel : ViewModelBase
    {
        private readonly IHistoriesApiService historyApi;
        private Dictionary<string, Brush> workoutNameToBrush = new();
        private int workoutBrushIdx = 0;
        private List<Brush> workoutBrushes = new()
        {
            new SolidColorBrush(Color.FromArgb("#FF8B1FA9")),
            new SolidColorBrush(Color.FromArgb("#FFD20100")),
            new SolidColorBrush(Color.FromArgb("#FFFC571D")),
            new SolidColorBrush(Color.FromArgb("#FF36B37B")),
            new SolidColorBrush(Color.FromArgb("#FF3D4FB5")),
            new SolidColorBrush(Color.FromArgb("#FFE47C73")),
            new SolidColorBrush(Color.FromArgb("#FF636363")),
            new SolidColorBrush(Color.FromArgb("#FF85461E")),
            new SolidColorBrush(Color.FromArgb("#FF0F8644")),
            new SolidColorBrush(Color.FromArgb("#FF01A1EF"))
        };

        [ObservableProperty]
        private DateTime minDateTime = DateTime.Now;

        [ObservableProperty]
        private DateTime maxDateTime = DateTime.Now;

        [ObservableProperty]
        private DateTime displayDate;

        [ObservableProperty]
        public ObservableCollection<SchedulerAppointment> workouts = new();

        public WorkoutCalendarViewModel(ILoggingService logger, IHistoriesApiService historyApi)
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
            MaxDateTime = allEventsInterval.InclusiveTo.Date;
            IsBusy = false;
        }

        [RelayCommand]
        private async Task WorkoutTapped(SchedulerTappedEventArgs args)
        {
            if (args == null || !args.Appointments.Any())
            {
                return;
            }

            var workout = args.Appointments.First() as SchedulerAppointment;
            var navData = new NavData.WorkoutSnapshotNavData(Convert.ToInt32(workout.Id));
            await Shell.Current.GoToAsync(nameof(WorkoutSnapshotView), true, navData.ToNavDataDictionary());
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
            var workouts = await historyApi.ReadWorkouts(dates.First(), dates.Last());
            var workoutEvents = new ObservableCollection<SchedulerAppointment>();
            foreach (var workout in workouts)
            {
                var ev = new SchedulerAppointment
                {
                    IsAllDay = true,
                    StartTime = workout.Date,
                    EndTime = workout.Date.AddHours(1),
                    Subject = workout.Name,
                    Id = workout.Id,
                    Background = GetWorkoutBrush(workout.Name)
                };

                workoutEvents.Add(ev);
            }

            Workouts = workoutEvents;
            IsBusy = false;
        }

        private Brush GetWorkoutBrush(string workoutName)
        {
            if (!workoutNameToBrush.ContainsKey(workoutName))
            {
                workoutNameToBrush.Add(workoutName, workoutBrushes[workoutBrushIdx++]);
                if (workoutBrushIdx == workoutBrushes.Count)
                {
                    workoutBrushIdx = 0;
                }
            }

            return workoutNameToBrush[workoutName];
        }
    }
}
