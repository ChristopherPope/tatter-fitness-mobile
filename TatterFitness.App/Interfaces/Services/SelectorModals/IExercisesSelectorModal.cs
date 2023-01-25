using static TatterFitness.App.Utils.SelectorModalDelegates;

namespace TatterFitness.App.Interfaces.Services.SelectorModals
{
    public interface IExercisesSelectorModal
    {
        Task ShowModal(IEnumerable<int> currentExerciseIds, ExercisesSelectionCompleted selectionCompleted);
        Task ShowModal(IEnumerable<int> currentExerciseIds, IEnumerable<int> requiredExerciseIds, ExercisesSelectionCompleted selectionCompleted);

        Task ShowModal(int currentExerciseId, ExercisesSelectionCompleted selectionCompleted);
    }
}
