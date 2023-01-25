using TatterFitness.App.ViewModels.WorkoutSnapshot;

namespace TatterFitness.App.Views.Workouts;

public partial class WorkoutSnapshotView : ContentPageBase<WorkoutSnapshotViewModel>
{
	public WorkoutSnapshotView(WorkoutSnapshotViewModel vm)
		:base(vm)
	{
		InitializeComponent();
	}
}