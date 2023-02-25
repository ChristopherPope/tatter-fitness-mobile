using TatterFitness.App.ViewModels.History.EventCalendar;

namespace TatterFitness.App.Views.History;

public partial class WorkoutCalendarView : ContentPageBase<WorkoutCalendarViewModel>
{
    public WorkoutCalendarView(WorkoutCalendarViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }
}