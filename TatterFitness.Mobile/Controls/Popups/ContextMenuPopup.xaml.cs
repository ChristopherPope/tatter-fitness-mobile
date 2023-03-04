using CommunityToolkit.Maui.Views;
using TatterFitness.Mobile.Models.Popups;

namespace TatterFitness.Mobile.Controls.Popups;

public partial class ContextMenuPopup : Popup
{
    public string Title { get; set; }

    public ContextMenuPopup(IEnumerable<ContextMenuButtonMetadata> buttonMetadata, string title)
    {
        InitializeComponent();
        var sizes = new PopupSizeConstants(DeviceDisplay.Current);
        Size = sizes.Medium;

        Title = title;
        foreach (var metadata in buttonMetadata)
        {
            var btn = new Button { Text = metadata.Title, CommandParameter = metadata };
            btn.Clicked += SelectMenuItem;

            if (metadata.IsEnabled)
            {
                buttonsContainer.Children.Add(btn);
            }
        }

        BindingContext = this;
    }

    private async void SelectMenuItem(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button == null)
        {
            return;
        }

        var buttonMetadata = button.CommandParameter as ContextMenuButtonMetadata;
        if (buttonMetadata.ConfirmDecisionMetadata != null && !(await ConfirmDecision(buttonMetadata.ConfirmDecisionMetadata)))
        {
            return;
        }

        Close(Convert.ToInt32(buttonMetadata.CommandParameter));
    }

    private async Task<bool> ConfirmDecision(DecisionPopupMetadata metadata)
    {
        var popup = new DecisionPopup(metadata);
        var choice = await Shell.Current.CurrentPage.ShowPopupAsync(popup);

        return Convert.ToBoolean(choice);
    }
}