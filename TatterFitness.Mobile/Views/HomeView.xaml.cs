using TatterFitness.Mobile.ViewModels.Home;

namespace TatterFitness.Mobile.Views;

public partial class HomeView : ContentPageBase<HomeViewModel>
{
    public HomeView(HomeViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }
}