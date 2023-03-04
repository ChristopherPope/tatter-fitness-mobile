using TatterFitness.Mobile.Interfaces.Services.ContextMenu;
using TatterFitness.Mobile.Models.Popups;
using TatterFitness.Mobile.ViewModels.Routines;

namespace TatterFitness.Mobile.Services.ContextMenu
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
