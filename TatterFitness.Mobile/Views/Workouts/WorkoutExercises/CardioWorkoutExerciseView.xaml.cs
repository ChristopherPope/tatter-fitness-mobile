using TatterFitness.App.ViewModels.Workouts.WorkoutExercises;

namespace TatterFitness.App.Views.Workouts.WorkoutExercises;

public partial class CardioWorkoutExerciseView : ContentPageBase<CardioWorkoutExerciseViewModel>
{
	public CardioWorkoutExerciseView(CardioWorkoutExerciseViewModel vm)
		: base(vm)
	{
		InitializeComponent();
	}
}