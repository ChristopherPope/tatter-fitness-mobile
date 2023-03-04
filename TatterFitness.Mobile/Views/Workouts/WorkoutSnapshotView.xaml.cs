using TatterFitness.Mobile.ViewModels.WorkoutSnapshot;

namespace TatterFitness.Mobile.Views.Workouts;

public partial class WorkoutSnapshotView : ContentPageBase<WorkoutSnapshotViewModel>
{
	public WorkoutSnapshotView(WorkoutSnapshotViewModel vm)
		:base(vm)
	{
		InitializeComponent();
	}
}