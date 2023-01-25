using TatterFitness.App.Interfaces.Services.ContextMenu;
using TatterFitness.App.Models.Popups;
using TatterFitness.App.ViewModels.Routines;

namespace TatterFitness.App.Services.ContextMenu
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
