using NodaTime;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Interfaces.Services.API;
using TatterFitness.App.Views.Workouts;

namespace TatterFitness.App.ViewModels.History.EventCalendar
{
    public partial class WorkoutEventViewModel : EventCalendarViewModelBase
    {
        private readonly IHistoriesApiService historyApi;

        public WorkoutEventViewModel(ILoggingService logger, IHistoriesApiService historyApi) : base(logger)
        {
            this.historyApi = historyApi;
            Title = "Workout History";
        }

        protected override async Task ProcessDayClicked(int eventId)
        {
            var navData = new NavData.WorkoutSnapshotNavData(eventId);
            await Shell.Current.GoToAsync(nameof(WorkoutSnapshotView), true, navData.ToNavDataDictionary());
        }

        protected override async Task<DateInterval> GetAllEventsInterval()
        {
            var range = await historyApi.ReadFirstAndLastWorkoutDates();

            return new DateInterval(LocalDate.FromDateTime(range.InclusiveFrom.Value), LocalDate.FromDateTime(range.InclusiveTo.Value));
        }

        protected override async Task<Dictionary<LocalDate, int>> LoadEvents(DateInterval interval)
        {
            var events = await historyApi.ReadWorkoutEvents(interval.Start.ToDateTimeUnspecified(), interval.End.ToDateTimeUnspecified());

            var eventLookup = new Dictionary<LocalDate, int>();
            foreach (var ev in events) 
            {
                if (ev.EventId == 0)
                {
                    continue;
                }

                var date = LocalDate.FromDateTime(ev.EventDate);
                if (!eventLookup.ContainsKey(date))
                {
                    eventLookup.Add(date, ev.EventId);
                }
            }

            return eventLookup;
        }
    }
}
