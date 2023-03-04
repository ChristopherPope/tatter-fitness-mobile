using TatterFitness.Mobile.ViewModels.Routines;

namespace TatterFitness.Mobile.Views.Routines;

public partial class EditRoutineView : ContentPageBase<EditRoutineViewModel>
{
    public EditRoutineView(EditRoutineViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }
}