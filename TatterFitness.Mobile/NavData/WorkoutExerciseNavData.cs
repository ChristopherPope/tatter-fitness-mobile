using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.NavData
{
    public class WorkoutExerciseNavData : NavDataBase
    {
        public WorkoutExercise WorkoutExercise { get; }
        public Workout Workout { get; }

        public WorkoutExerciseNavData(WorkoutExercise workoutExercise, Workout workout)
        {
            WorkoutExercise = workoutExercise;
            Workout = workout;
        }
    }
}
