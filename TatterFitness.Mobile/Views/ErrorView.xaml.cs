using TatterFitness.App.ViewModels;

namespace TatterFitness.App.Views;

public partial class ErrorView : ContentPageBase<ErrorViewModel>
{
	public ErrorView(ErrorViewModel vm)
		:base(vm)
	{
		InitializeComponent();
	}
}