using TatterFitness.App.ViewModels.History.ExHistory;

namespace TatterFitness.App.Views.History;

public partial class ExerciseHistoryView : ContentPageBase<ExerciseHistoryViewModel>
{
	public ExerciseHistoryView(ExerciseHistoryViewModel vm)
		: base(vm)
	{
		InitializeComponent();
	}
}