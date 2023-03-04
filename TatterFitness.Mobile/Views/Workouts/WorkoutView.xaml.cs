using TatterFitness.Mobile.ViewModels.Workouts;

namespace TatterFitness.Mobile.Views.Workouts;

public partial class WorkoutView : ContentPageBase<WorkoutViewModel>
{
    public WorkoutView(WorkoutViewModel vm)
        : base(vm)
    {
        InitializeComponent();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("Alert", "You have been alerted", "OK");
    }
}