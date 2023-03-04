using TatterFitness.Mobile.ViewModels.Select;

namespace TatterFitness.Mobile.Views.Select;

public partial class SelectExercisesView : ContentPageBase<SelectExercisesViewModel>
{
    public SelectExercisesView(SelectExercisesViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed() => false;
}