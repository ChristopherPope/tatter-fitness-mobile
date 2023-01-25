using CommunityToolkit.Maui.Views;
using TatterFitness.App.Controls.Popups;
using TatterFitness.App.Interfaces.Services.ContextMenu;
using TatterFitness.App.Models.Popups;

namespace TatterFitness.App.Services.ContextMenu
{
    public class ContextMenuServiceBaseBase : IContextMenuService
    {
        protected async Task<int> ShowMenu(IEnumerable<ContextMenuButtonMetadata> buttons, string title)
        {
            var contextMenu = new ContextMenuPopup(buttons, title);
            var choice = await Shell.Current.CurrentPage.ShowPopupAsync(contextMenu);

            if (choice == null)
            {
                choice = IContextMenuService.CancelMenuId;
            }

            return Convert.ToInt32(choice);
        }
    }
}
