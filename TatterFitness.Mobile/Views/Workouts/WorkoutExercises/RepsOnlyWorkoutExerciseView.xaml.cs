using TatterFitness.Mobile.ViewModels.Workouts.WorkoutExercises;

namespace TatterFitness.Mobile.Views.Workouts.WorkoutExercises;

public partial class RepsOnlyWorkoutExerciseView : ContentPageBase<RepsOnlyWorkoutExerciseViewModel>
{
	public RepsOnlyWorkoutExerciseView(RepsOnlyWorkoutExerciseViewModel vm)
		: base(vm)
	{
		InitializeComponent();
	}
}