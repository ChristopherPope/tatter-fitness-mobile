using TatterFitness.App.ViewModels.Workouts.WorkoutExercises;

namespace TatterFitness.App.Views.Workouts.WorkoutExercises;

public partial class DurationAndWeightWorkoutExerciseView : ContentPageBase<DurationAndWeightWorkoutExerciseViewModel>
{
	public DurationAndWeightWorkoutExerciseView(DurationAndWeightWorkoutExerciseViewModel vm)
		: base(vm)
	{
		InitializeComponent();
	}
}