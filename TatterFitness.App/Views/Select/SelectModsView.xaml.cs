using TatterFitness.App.ViewModels.Select;

namespace TatterFitness.App.Views.Select;

public partial class SelectModsView : ContentPageBase<SelectModsViewModel>
{
    public SelectModsView(SelectModsViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed() => false;
}