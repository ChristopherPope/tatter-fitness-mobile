using TatterFitness.App.ViewModels.History.EventCalendar;

namespace TatterFitness.App.Views.History;

public partial class WorkoutEventView : ContentPageBase<WorkoutEventViewModel>
{
	public WorkoutEventView(WorkoutEventViewModel vm)
		: base(vm)
	{
		InitializeComponent();
	}
}