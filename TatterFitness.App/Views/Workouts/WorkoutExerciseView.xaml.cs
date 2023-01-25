using TatterFitness.App.ViewModels.Workouts;

namespace TatterFitness.App.Views.Workouts;

public partial class WorkoutExerciseView : ContentPageBase<WorkoutExerciseViewModel>
{
	public WorkoutExerciseView(WorkoutExerciseViewModel vm)
		: base(vm)
	{
		InitializeComponent();
	}
}