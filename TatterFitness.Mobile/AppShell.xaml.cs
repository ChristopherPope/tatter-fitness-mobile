using TatterFitness.Mobile.Views;
using TatterFitness.Mobile.Views.History;
using TatterFitness.Mobile.Views.Routines;
using TatterFitness.Mobile.Views.Select;
using TatterFitness.Mobile.Views.Workouts;
using TatterFitness.Mobile.Views.Workouts.WorkoutExercises;

namespace TatterFitness.Mobile
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
            Routing.RegisterRoute(nameof(WorkoutView), typeof(WorkoutView));
            Routing.RegisterRoute(nameof(WorkoutCalendarView), typeof(WorkoutCalendarView));
            Routing.RegisterRoute(nameof(ExerciseHistoryView), typeof(ExerciseHistoryView));
            Routing.RegisterRoute(nameof(WorkoutSnapshotView), typeof(WorkoutSnapshotView));

            Routing.RegisterRoute(nameof(CardioWorkoutExerciseView), typeof(CardioWorkoutExerciseView));
            Routing.RegisterRoute(nameof(RepsAndWeightWorkoutExerciseView), typeof(RepsAndWeightWorkoutExerciseView));
            Routing.RegisterRoute(nameof(RepsOnlyWorkoutExerciseView), typeof(RepsOnlyWorkoutExerciseView));
            Routing.RegisterRoute(nameof(DurationAndWeightWorkoutExerciseView), typeof(DurationAndWeightWorkoutExerciseView));

        }
    }
}