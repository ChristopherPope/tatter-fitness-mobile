using CommunityToolkit.Maui.Views;
using TatterFitness.App.Models.Popups;

namespace TatterFitness.App.Controls.Popups;

public partial class NotesEntryPopup : Popup
{
    public string Title { get; set; }
    public string Prompt { get; set; }
    public string Notes { get; set; }

    public NotesEntryPopup(ValueEntryPopupMetadata metadata)
    {
        InitializeComponent();
        var sizes = new PopupSizeConstants(DeviceDisplay.Current);
        Size = sizes.Medium;

        Title = metadata.Title;
        Prompt = metadata.Prompt;
        Notes = metadata.Value;

        BindingContext = this;
    }

    private void OkButtonPressed(object sender, EventArgs e) => Close(Notes);
    private void CancelButtonPressed(object sender, EventArgs e) => Close(null);

}