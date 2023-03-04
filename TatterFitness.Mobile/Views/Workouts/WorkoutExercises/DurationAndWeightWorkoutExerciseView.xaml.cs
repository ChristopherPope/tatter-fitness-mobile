using TatterFitness.Mobile.ViewModels.Workouts.WorkoutExercises;

namespace TatterFitness.Mobile.Views.Workouts.WorkoutExercises;

public partial class DurationAndWeightWorkoutExerciseView : ContentPageBase<DurationAndWeightWorkoutExerciseViewModel>
{
	public DurationAndWeightWorkoutExerciseView(DurationAndWeightWorkoutExerciseViewModel vm)
		: base(vm)
	{
		InitializeComponent();
	}
}