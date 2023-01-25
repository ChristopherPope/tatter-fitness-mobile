using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.NavData;
using TatterFitness.App.Views;

namespace TatterFitness.App.Services.SelectorModals
{
    public abstract class SelectorModalBase
    {
        protected readonly ILoggingService logger;

        protected SelectorModalBase(ILoggingService logger)
        {
            this.logger = logger;
        }

        protected async Task HandleException(Exception ex)
        {
            var crlf = Environment.NewLine;
            var error = string.Empty;

            try
            {
                error = ex.ToString();
                error = error.Replace("at ", $"{crlf}at ");
                var navData = new ErrorViewNavData(error);
                logger.Error(navData.ErrorMessage);

                await Shell.Current.GoToAsync(nameof(ErrorView), true, navData.ToNavDataDictionary());
            }
            catch (Exception failureEx)
            {
                logger.Error($"Unable to show failure - {failureEx.Message}{crlf}{error}");
            }
        }
    }
}
