using TatterFitness.App.Interfaces.Services.ContextMenu;
using TatterFitness.App.Models.Popups;
using TatterFitness.App.ViewModels.WorkoutSnapshot;

namespace TatterFitness.App.Services.ContextMenu
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
