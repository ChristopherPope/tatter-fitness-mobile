using TatterFitness.Mobile.ViewModels.History.EventCalendar;

namespace TatterFitness.Mobile.Views.History;

public partial class WorkoutCalendarView : ContentPageBase<WorkoutCalendarViewModel>
{
    public WorkoutCalendarView(WorkoutCalendarViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }
}