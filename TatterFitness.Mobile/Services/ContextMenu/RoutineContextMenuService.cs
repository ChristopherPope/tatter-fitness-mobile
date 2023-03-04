using TatterFitness.Mobile.Interfaces.Services.ContextMenu;
using TatterFitness.Mobile.Models.Popups;
using TatterFitness.Mobile.ViewModels.Routines;

namespace TatterFitness.Mobile.Services.ContextMenu
{
    public class RoutineContextMenuService : ContextMenuServiceBaseBase, IRoutineContextMenuService
    {
        public async Task<int> Show(ShowRoutinesCardViewModel routineVm)
        {
            var buttons = new List<ContextMenuButtonMetadata>
            {
                    new ContextMenuButtonMetadata {
                        Title = "Rename Routine",
                        CommandParameter = IRoutineContextMenuService.RenameRoutineMenuId
                    },

                    new ContextMenuButtonMetadata {
                        Title = "Workout Routine",
                        CommandParameter = IRoutineContextMenuService.WorkoutRoutineMenuId
                    },

                    new ContextMenuButtonMetadata {
                        Title = "Delete Routine",
                        ConfirmDecisionMetadata = new DecisionPopupMetadata
                        {
                            Prompt = $"Are you sure you wish to delete the {routineVm.RoutineName} routine?",
                            Title = $"Delete {routineVm.RoutineName}"
                        },
                        CommandParameter = IRoutineContextMenuService.DeleteRoutineMenuId
                    }
            };

            return await ShowMenu(buttons, $"{routineVm.RoutineName}");
        }
    }
}
