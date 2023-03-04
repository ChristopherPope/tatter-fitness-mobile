using TatterFitness.Mobile.ViewModels.Select;

namespace TatterFitness.Mobile.Views.Select;

public partial class SelectModsView : ContentPageBase<SelectModsViewModel>
{
    public SelectModsView(SelectModsViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed() => false;
}