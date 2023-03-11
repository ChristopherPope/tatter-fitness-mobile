using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.Models
{
    public class FTOResults
    {
        public List<WorkoutExerciseSet> ExerciseSets { get; set; } = new List<WorkoutExerciseSet>();
        public int TrainingMax { get; set; }
        public int WeekNumber { get; set; }
    }
}
