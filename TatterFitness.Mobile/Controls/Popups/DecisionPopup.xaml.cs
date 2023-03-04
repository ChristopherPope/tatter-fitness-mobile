using CommunityToolkit.Maui.Views;
using TatterFitness.Mobile.Models.Popups;

namespace TatterFitness.Mobile.Controls.Popups;

public partial class DecisionPopup : Popup
{
    public string Title { get; set; }
    public string Prompt { get; set; }
    public string YesButtonTitle { get; set; }
    public string NoButtonTitle { get; set; }

    public DecisionPopup(DecisionPopupMetadata metadata)
    {
        InitializeComponent();
        var sizes = new PopupSizeConstants(DeviceDisplay.Current);
        Size = sizes.Medium;

        Title = metadata.Title;
        Prompt = metadata.Prompt;
        YesButtonTitle = metadata.YesButtonTitle;
        NoButtonTitle = metadata.NoButtonTitle;

        BindingContext = this;
    }

    private void YesButtonPressed(object sender, EventArgs e) => Close(true);
    private void NoButtonPressed(object sender, EventArgs e) => Close(false);

}