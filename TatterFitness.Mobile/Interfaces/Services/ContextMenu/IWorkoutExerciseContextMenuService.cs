using TatterFitness.Mobile.ViewModels.Workouts;

namespace TatterFitness.Mobile.Interfaces.Services.ContextMenu
{
    public interface IWorkoutExerciseContextMenuService : IContextMenuService
    {
        const int RemoveExerciseMenuId = 1;
        const int EditModsMenuId = 2;
        const int EditNotesMenuId = 3;
        const int ShowExerciseHistoryMenuId = 4;
        Task<int> Show(WorkoutCardViewModel exerciseVm);
    }
}
