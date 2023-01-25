using TatterFitness.App.ViewModels.Routines;

namespace TatterFitness.App.Views.Routines;

public partial class ShowRoutinesView : ContentPageBase<ShowRoutinesViewModel>
{
    public ShowRoutinesView(ShowRoutinesViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }
}