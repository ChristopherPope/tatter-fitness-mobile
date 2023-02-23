using TatterFitness.App.ViewModels.Workouts.WorkoutExercises;

namespace TatterFitness.App.Views.Workouts.WorkoutExercises;

public partial class RepsAndWeightWorkoutExerciseView : ContentPageBase<RepsAndWeightWorkoutExerciseViewModel>
{
	public RepsAndWeightWorkoutExerciseView(RepsAndWeightWorkoutExerciseViewModel vm)
		: base(vm)
	{
		InitializeComponent();
	}
}