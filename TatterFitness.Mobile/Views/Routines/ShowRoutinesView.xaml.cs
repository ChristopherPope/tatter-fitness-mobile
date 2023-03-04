using TatterFitness.Mobile.ViewModels.Routines;

namespace TatterFitness.Mobile.Views.Routines;

public partial class ShowRoutinesView : ContentPageBase<ShowRoutinesViewModel>
{
    public ShowRoutinesView(ShowRoutinesViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }
}