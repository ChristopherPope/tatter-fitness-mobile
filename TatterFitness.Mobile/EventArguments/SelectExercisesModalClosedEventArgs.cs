using TatterFitness.Models.Exercises;

namespace TatterFitness.App.EventArguments
{
    public class SelectExercisesModalClosedEventArgs : EventArgs
    {
        public IEnumerable<Exercise> ExercisesToAdd { get; } = Enumerable.Empty<Exercise>();
        public IEnumerable<Exercise> ExercisesToRemove { get; } = Enumerable.Empty<Exercise>();

        public SelectExercisesModalClosedEventArgs()
        {
        }

        public SelectExercisesModalClosedEventArgs(IEnumerable<Exercise> exercisesToAdd, IEnumerable<Exercise> exercisesToRemove)
        {
            ExercisesToAdd = exercisesToAdd;
            ExercisesToRemove = exercisesToRemove;
        }
    }
}
