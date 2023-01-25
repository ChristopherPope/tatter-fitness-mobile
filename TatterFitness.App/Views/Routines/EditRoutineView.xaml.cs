using TatterFitness.App.ViewModels.Routines;

namespace TatterFitness.App.Views.Routines;

public partial class EditRoutineView : ContentPageBase<EditRoutineViewModel>
{
    public EditRoutineView(EditRoutineViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }
}