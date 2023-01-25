using CommunityToolkit.Mvvm.ComponentModel;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.NavData;

namespace TatterFitness.App.ViewModels
{
    public partial class ErrorViewModel : ViewModelBase, IQueryAttributable
    {
        public const string ErrorMessageKey = "ErrorMessageKey";
        [ObservableProperty]
        private string errorMessage = "???";

        public ErrorViewModel(ILoggingService logger)
            : base(logger)
        {
            Title = "Uh-Oh!";
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var navData = NavDataBase.FromNavDataDictionary<ErrorViewNavData>(query);
            ErrorMessage = navData?.ErrorMessage ?? "ERROR";
        }

        protected override Task PerformLoadViewData()
        {
            return Task.CompletedTask;
        }
    }
}
