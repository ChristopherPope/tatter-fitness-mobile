using CommunityToolkit.Maui.Views;
using TatterFitness.Mobile.Controls.Popups;
using TatterFitness.Mobile.Interfaces.Services.ContextMenu;
using TatterFitness.Mobile.Models.Popups;

namespace TatterFitness.Mobile.Services.ContextMenu
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
