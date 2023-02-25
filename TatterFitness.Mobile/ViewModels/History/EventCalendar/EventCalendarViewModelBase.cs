using CommunityToolkit.Mvvm.ComponentModel;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.Models.Workouts;

namespace TatterFitness.App.ViewModels.History.EventCalendar
{
    public abstract partial class EventCalendarViewModelBase : ViewModelBase
    {
        [ObservableProperty]
        private DateTime minDateTime = DateTime.Now;

        [ObservableProperty]
        private DateTime maxDateTime = DateTime.Now;

        [ObservableProperty]
        private DateTime displayDate;

        [ObservableProperty]
        public ObservableCollection<SchedulerAppointment> events = new();

        public EventCalendarViewModelBase(ILoggingService logger) : base(logger)
        {
        }

        protected abstract Task ProcessDayClicked(int eventId);
        protected abstract Task<IEnumerable<SchedulerAppointment>> LoadEvents(DateTime inclusiveFrom, DateTime inclusiveTo);
        protected abstract Task<WorkoutDateRange> GetAllEventsInterval();

        protected override async Task PerformLoadViewData()
        {
            var allEventsInterval = await GetAllEventsInterval();
            MinDateTime = allEventsInterval.InclusiveFrom.Date;
            MaxDateTime = allEventsInterval.InclusiveTo.Date;

            var events = await LoadEvents(allEventsInterval.InclusiveFrom, allEventsInterval.InclusiveTo);
            foreach (var ev in events)
            {
                Events.Add(ev);
            }
        }
    }
}
