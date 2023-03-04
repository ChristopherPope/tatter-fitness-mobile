using TatterFitness.Mobile.Interfaces.Services.ContextMenu;
using TatterFitness.Mobile.Models.Popups;
using TatterFitness.Mobile.ViewModels.WorkoutSnapshot;

namespace TatterFitness.Mobile.Services.ContextMenu
{
    public class WorkoutSnapshotContextMenuService : ContextMenuServiceBaseBase, IWorkoutSnapshotContextMenuService
    {
        public async Task<int> Show(WorkoutSnapshotCardViewModel exerciseVm)
        {
            var buttons = new List<ContextMenuButtonMetadata>
            {
                    new ContextMenuButtonMetadata {
                        Title = "View Exercise History",
                        CommandParameter = IWorkoutExerciseContextMenuService.ShowExerciseHistoryMenuId
                    }
            };

            return await ShowMenu(buttons, exerciseVm.ExerciseName);
        }
    }
}
