using TatterFitness.Mobile.ViewModels.Workouts.WorkoutExercises;

namespace TatterFitness.Mobile.Views.Workouts.WorkoutExercises;

public partial class CardioWorkoutExerciseView : ContentPageBase<CardioWorkoutExerciseViewModel>
{
	public CardioWorkoutExerciseView(CardioWorkoutExerciseViewModel vm)
		: base(vm)
	{
		InitializeComponent();
	}
}