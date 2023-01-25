using TatterFitness.App.ViewModels.Select;

namespace TatterFitness.App.Views.Select;

public partial class SelectExercisesView : ContentPageBase<SelectExercisesViewModel>
{
    public SelectExercisesView(SelectExercisesViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed() => false;
}