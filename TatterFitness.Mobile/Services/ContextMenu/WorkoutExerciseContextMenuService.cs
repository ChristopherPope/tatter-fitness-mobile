using TatterFitness.Mobile.Interfaces.Services.ContextMenu;
using TatterFitness.Mobile.Models.Popups;
using TatterFitness.Mobile.ViewModels.Workouts;

namespace TatterFitness.Mobile.Services.ContextMenu
{
    public class WorkoutExerciseContextMenuService : ContextMenuServiceBaseBase, IWorkoutExerciseContextMenuService
    {
        public async Task<int> Show(WorkoutCardViewModel exerciseVm)
        {
            var buttons = new List<ContextMenuButtonMetadata>
            {
                    new ContextMenuButtonMetadata { 
                        Title = "Remove Exercise", 
                        CommandParameter = IWorkoutExerciseContextMenuService.RemoveExerciseMenuId, 
                        IsEnabled = !exerciseVm.HasCompletedSets() 
                    },

                    new ContextMenuButtonMetadata {
                        Title = "Edit Mods",
                        CommandParameter = IWorkoutExerciseContextMenuService.EditModsMenuId
                    },

                    new ContextMenuButtonMetadata {
                        Title = "Edit Notes",
                        CommandParameter = IWorkoutExerciseContextMenuService.EditNotesMenuId
                    },

                    new ContextMenuButtonMetadata {
                        Title = "View History",
                        CommandParameter = IWorkoutExerciseContextMenuService.ShowExerciseHistoryMenuId
                    }
            };

            return await ShowMenu(buttons, exerciseVm.ExerciseName);
        }
    }
}
