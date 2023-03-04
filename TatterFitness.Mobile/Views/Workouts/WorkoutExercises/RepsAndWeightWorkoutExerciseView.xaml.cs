using TatterFitness.Mobile.ViewModels.Workouts.WorkoutExercises;

namespace TatterFitness.Mobile.Views.Workouts.WorkoutExercises;

public partial class RepsAndWeightWorkoutExerciseView : ContentPageBase<RepsAndWeightWorkoutExerciseViewModel>
{
	public RepsAndWeightWorkoutExerciseView(RepsAndWeightWorkoutExerciseViewModel vm)
		: base(vm)
	{
		InitializeComponent();
	}
}