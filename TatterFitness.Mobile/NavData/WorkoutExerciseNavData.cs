using TatterFitness.Models.Enums;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.NavData
{
    public class WorkoutExerciseNavData : NavDataBase
    {
        public WorkoutExercise WorkoutExercise { get; }

        public WorkoutExerciseNavData(WorkoutExercise workoutExercise)
        {
            WorkoutExercise = workoutExercise;
        }
    }
}
