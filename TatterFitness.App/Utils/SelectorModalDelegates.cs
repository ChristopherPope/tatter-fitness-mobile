using TatterFitness.Models.Exercises;

namespace TatterFitness.App.Utils
{
    public class SelectorModalDelegates
    {
        public delegate Task ModsSelectionCompleted(IEnumerable<ExerciseModifier> modsToAdd, IEnumerable<ExerciseModifier> modsToRemove);

        public delegate Task ExercisesSelectionCompleted(IEnumerable<Exercise> exercisesToAdd, IEnumerable<Exercise> exercisesToRemove);
    }
}
