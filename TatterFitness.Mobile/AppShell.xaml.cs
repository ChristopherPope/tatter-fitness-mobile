using TatterFitness.App.Views.Routines;
using TatterFitness.App.Views.Select;
using TatterFitness.App.Views.Workouts;
using TatterFitness.App.Views;
using TatterFitness.App.Views.History;

namespace TatterFitness.App
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute(nameof(ErrorView), typeof(ErrorView));
			Routing.RegisterRoute(nameof(EditRoutineView), typeof(EditRoutineView));
			Routing.RegisterRoute(nameof(SelectModsView), typeof(SelectModsView));
			Routing.RegisterRoute(nameof(SelectExercisesView), typeof(SelectExercisesView));
			Routing.RegisterRoute(nameof(WorkoutExerciseView), typeof(WorkoutExerciseView));
			Routing.RegisterRoute(nameof(WorkoutView), typeof(WorkoutView));
			Routing.RegisterRoute(nameof(WorkoutEventView), typeof(WorkoutEventView));
			Routing.RegisterRoute(nameof(ExerciseHistoryView), typeof(ExerciseHistoryView));
			Routing.RegisterRoute(nameof(WorkoutSnapshotView), typeof(WorkoutSnapshotView));
        }
	}
}