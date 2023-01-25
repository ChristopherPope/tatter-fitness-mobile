using CommunityToolkit.Mvvm.ComponentModel;

namespace TatterFitness.App.ViewModels.History.EventCalendar
{
    [INotifyPropertyChanged]
    public partial class EventCalendarDayViewModel 
    {
        [ObservableProperty]
        private int dayOfMonth;

        [ObservableProperty]
        private int eventId;

        [ObservableProperty]
        private bool hasEvent;

        [ObservableProperty]
        private bool isSelectedMonth;
    }
}
