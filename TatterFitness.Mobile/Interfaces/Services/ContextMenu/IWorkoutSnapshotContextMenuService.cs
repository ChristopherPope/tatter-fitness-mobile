using TatterFitness.Mobile.ViewModels.WorkoutSnapshot;

namespace TatterFitness.Mobile.Interfaces.Services.ContextMenu
{
    public interface IWorkoutSnapshotContextMenuService : IContextMenuService
    {
        const int ShowExerciseHistoryMenuId = 1;
        Task<int> Show(WorkoutSnapshotCardViewModel exerciseVm);
    }
}
