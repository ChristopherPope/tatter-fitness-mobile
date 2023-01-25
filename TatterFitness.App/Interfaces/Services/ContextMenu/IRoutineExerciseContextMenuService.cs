using TatterFitness.App.ViewModels.Routines;

namespace TatterFitness.App.Interfaces.Services.ContextMenu
{ // todo: look at removing these explicit context menu services and replacing them with a dynamic one
    public interface IRoutineExerciseContextMenuService : IContextMenuService
    {
        const int RemoveExerciseMenuId = 1;
        const int ShowExerciseHistoryMenuId = 2;

        Task<int> Show(EditRoutineCardViewModel cardVm);
    }
}
