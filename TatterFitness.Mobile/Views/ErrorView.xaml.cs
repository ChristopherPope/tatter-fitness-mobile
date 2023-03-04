using TatterFitness.Mobile.ViewModels;

namespace TatterFitness.Mobile.Views;

public partial class ErrorView : ContentPageBase<ErrorViewModel>
{
	public ErrorView(ErrorViewModel vm)
		:base(vm)
	{
		InitializeComponent();
	}
}