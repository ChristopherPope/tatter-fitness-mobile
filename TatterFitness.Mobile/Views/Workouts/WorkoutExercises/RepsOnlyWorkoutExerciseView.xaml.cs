using TatterFitness.App.ViewModels.Workouts.WorkoutExercises;

namespace TatterFitness.App.Views.Workouts.WorkoutExercises;

public partial class RepsOnlyWorkoutExerciseView : ContentPageBase<RepsOnlyWorkoutExerciseViewModel>
{
	public RepsOnlyWorkoutExerciseView(RepsOnlyWorkoutExerciseViewModel vm)
		: base(vm)
	{
		InitializeComponent();
	}
}