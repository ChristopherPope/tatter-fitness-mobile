using TatterFitness.App.ViewModels.WorkoutSnapshot;

namespace TatterFitness.App.Interfaces.Services.ContextMenu
{
    public interface IWorkoutSnapshotContextMenuService : IContextMenuService
    {
        const int ShowExerciseHistoryMenuId = 1;
        Task<int> Show(WorkoutSnapshotCardViewModel exerciseVm);
    }
}
