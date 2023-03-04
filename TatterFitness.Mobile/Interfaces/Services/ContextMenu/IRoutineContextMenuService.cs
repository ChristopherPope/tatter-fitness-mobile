using TatterFitness.Mobile.ViewModels.Routines;

namespace TatterFitness.Mobile.Interfaces.Services.ContextMenu
{
    public interface IRoutineContextMenuService : IContextMenuService
    {
        const int RenameRoutineMenuId = 1;
        const int DeleteRoutineMenuId = 2;
        const int WorkoutRoutineMenuId = 3;

        Task<int> Show(ShowRoutinesCardViewModel routineVm);
    }
}
