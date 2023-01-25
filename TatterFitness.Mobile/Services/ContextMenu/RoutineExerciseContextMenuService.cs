using TatterFitness.App.Interfaces.Services.ContextMenu;
using TatterFitness.App.Models.Popups;
using TatterFitness.App.ViewModels.Routines;

namespace TatterFitness.App.Services.ContextMenu
{
    public class RoutineExerciseContextMenuService : ContextMenuServiceBaseBase, IRoutineExerciseContextMenuService
    {
        public async Task<int> Show(EditRoutineCardViewModel cardVm)
        {
            var buttons = new List<ContextMenuButtonMetadata>
            {
                    new ContextMenuButtonMetadata {
                        Title = "Remove Exercise",
                        CommandParameter = IRoutineExerciseContextMenuService.RemoveExerciseMenuId
                    },

                    new ContextMenuButtonMetadata {
                        Title = "View History",
                        CommandParameter = IRoutineExerciseContextMenuService.ShowExerciseHistoryMenuId
                    }
            };

            return await ShowMenu(buttons, $"{cardVm.Name}");
        }
    }
}
