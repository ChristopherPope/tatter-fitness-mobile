using CommunityToolkit.Maui.Views;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Models.Popups;

namespace TatterFitness.App.Controls.Popups;

public partial class NotesPopup : Popup
{
    public NotesPopup(NotesPopupMetadata metadata)
    {
        InitializeComponent();

        var sizes = new PopupSizeConstants(DeviceDisplay.Current);
        Size = sizes.Medium;

        title.Text = $"{metadata.ExerciseName} Notes";
        notes.Text = metadata.Notes;
    }

    private void OkButtonPressed(object sender, EventArgs e) => Close(null);

}