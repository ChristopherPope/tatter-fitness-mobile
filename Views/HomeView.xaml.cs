using TatterFitness.App.ViewModels.Home;

namespace TatterFitness.App.Views;

public partial class HomeView : ContentPageBase<HomeViewModel>
{
    public HomeView(HomeViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }
}