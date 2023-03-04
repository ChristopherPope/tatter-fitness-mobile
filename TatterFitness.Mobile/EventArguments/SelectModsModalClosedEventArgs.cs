using TatterFitness.Models.Exercises;

namespace TatterFitness.Mobile.EventArguments
{
    public class SelectModsModalClosedEventArgs : EventArgs
    {
        public IEnumerable<ExerciseModifier> ModifiersToAdd { get; } = Enumerable.Empty<ExerciseModifier>();
        public IEnumerable<ExerciseModifier> ModifiersToRemove { get; } = Enumerable.Empty<ExerciseModifier>();
        
        public SelectModsModalClosedEventArgs()
        {
        }

        public SelectModsModalClosedEventArgs(IEnumerable<ExerciseModifier> modifiersToAdd, IEnumerable<ExerciseModifier> modifiersToRemove)
        {
            ModifiersToAdd = modifiersToAdd;
            ModifiersToRemove = modifiersToRemove;
        }
    }
}
