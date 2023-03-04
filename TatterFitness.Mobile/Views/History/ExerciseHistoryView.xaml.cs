using TatterFitness.Mobile.ViewModels.History.ExHistory;

namespace TatterFitness.Mobile.Views.History;

public partial class ExerciseHistoryView : ContentPageBase<ExerciseHistoryViewModel>
{
	public ExerciseHistoryView(ExerciseHistoryViewModel vm)
		: base(vm)
	{
		InitializeComponent();
	}
}