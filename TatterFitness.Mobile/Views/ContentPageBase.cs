using TatterFitness.App.ViewModels;

namespace TatterFitness.App.Views
{
    public abstract class ContentPageBase<T> : ContentPage where T : ViewModelBase
    {
        public T ViewModel { get; private set; }

        public ContentPageBase(T viewModel)
        {
            ViewModel = viewModel;
            BindingContext = viewModel;

            Appearing += OnViewAppearing;
            TaskScheduler.UnobservedTaskException += OnUnobservedException;
        }

        private void OnUnobservedException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var s = e.Exception.Message;
        }

        private async void OnViewAppearing(object sender, EventArgs e)
        {
            await ViewModel.LoadViewData();
        }
    }
}