using CommunityToolkit.Maui.Views;
using TatterFitness.Mobile.Models.Popups;

namespace TatterFitness.Mobile.Controls.Popups;

public partial class ValueEntryPopup : Popup
{
    public string Title { get; set; }
    public string Prompt { get; set; }
    public string Value { get; set; }
    public bool DoShowPrompt { get; set; }

    public ValueEntryPopup(ValueEntryPopupMetadata metadata)
    {
        InitializeComponent();
        var sizes = new PopupSizeConstants(DeviceDisplay.Current);
        Size = sizes.Medium;

        Title = metadata.Title;
        Prompt = metadata.Prompt;
        Value = metadata.Value;
        DoShowPrompt = !String.IsNullOrEmpty(Prompt);

        BindingContext = this;
    }

    private void OkButtonPressed(object sender, EventArgs e) => Close(Value);
    private void CancelButtonPressed(object sender, EventArgs e) => Close(null);

}