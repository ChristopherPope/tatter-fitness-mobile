using CommunityToolkit.Mvvm.ComponentModel;
using Flurl.Http;
using System.ComponentModel;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.NavData;
using TatterFitness.Mobile.Views;

namespace TatterFitness.Mobile.ViewModels
{
    [INotifyPropertyChanged]
    public abstract partial class ViewModelBase
    {
        protected readonly ILoggingService logger;
        [ObservableProperty]
        private string title = string.Empty;
        private bool hasLoadedViewData = false;
        private static readonly string crlf = Environment.NewLine;

        [ObservableProperty]
        private bool isBusy;

        public async Task LoadViewData()
        {
            try
            {
                if (hasLoadedViewData)
                {
                    await RefreshView();
                    return;
                }

                await PerformLoadViewData();
                hasLoadedViewData = true;
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        protected abstract Task PerformLoadViewData();

        public virtual Task RefreshView()
        {
            return Task.CompletedTask;
        }

        protected ViewModelBase(ILoggingService logger)
        {
            this.logger = logger;
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected async Task HandleExceptionAsync(Exception ex)
        {
            var crlf = Environment.NewLine;
            var error = string.Empty;

            try
            {
                error = $"{ex.Message}{crlf}";

                var flurlEx = ex as FlurlHttpException;
                if (flurlEx != null)
                {
                    error += FormatFlurlEx(flurlEx);
                }

                error += FormatCallStack(ex);
                var navData = new ErrorViewNavData(error);
                logger.Error(navData.ErrorMessage);

                await Shell.Current.GoToAsync(nameof(ErrorView), true, navData.ToNavDataDictionary());
            }
            catch (Exception failureEx)
            {
                logger.Error($"Unable to show failure - {failureEx.Message}{crlf}{error}");
            }
        }

        private string FormatFlurlEx(FlurlHttpException ex)
        {
            return $"{crlf}REQUEST BODY:{crlf}{ex.Call.RequestBody}{crlf}";
        }

        private string FormatCallStack(Exception ex)
        {
            var callStack = ex.ToString().Replace("at ", $"{crlf}at ");
            return $"{crlf}{callStack}{crlf}";
        }

        protected void HandleException(Exception ex)
        {
            var crlf = Environment.NewLine;
            var error = string.Empty;

            try
            {
                error = ex.ToString();
                error = error.Replace("at ", $"{crlf}at ");
                logger.Error($"Unable to show failure - {crlf}{error}");
            }
            catch (Exception failureEx)
            {
                logger.Error($"Unable to show failure - {failureEx.Message}{crlf}{error}");
            }
        }
    }
}
