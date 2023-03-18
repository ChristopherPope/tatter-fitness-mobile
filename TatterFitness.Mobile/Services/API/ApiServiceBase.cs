using Flurl.Http;
using Flurl.Http.Configuration;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Models;
using TatterFitness.Mobile.NavData;
using TatterFitness.Mobile.Views;

namespace TatterFitness.Mobile.Services.API
{
    public abstract class ApiServiceBase
    {
        protected readonly string entityName;
        protected readonly ILoggingService logger;
        protected readonly IFlurlClientFactory flurlClientFactory;
        protected readonly IFlurlClient flurlClient;

        protected ApiServiceBase(string entityName, ILoggingService logger, IFlurlClientFactory flurlClientFactory)
        {
            this.entityName = entityName;
            this.logger = logger;
            this.flurlClientFactory = flurlClientFactory;

#if DEBUG
            // To connect to debug API, must run 
            //	 iisexpress-proxy 50662 to 88

            flurlClient = flurlClientFactory.Get("http://10.0.2.2:88/api/"); // DEBUG DB on emulator
            //flurlClient = flurlClientFactory.Get("http://192.168.1.10/api/"); // PROD DB on device
#else
            //flurlClient = flurlClientFactory.Get("http://192.168.1.10:88/api/"); // DEBUG DB on device
            flurlClient = flurlClientFactory.Get("http://192.168.1.10/api/"); // PROD DB on device
            //flurlClient = flurlClientFactory.Get("http://10.0.2.2:88/api/"); // DEBUG DB on emulator
#endif
        }

        protected IFlurlRequest CreateRequest(params object[] urlSegments)
        {
            var segments = urlSegments.ToList();
            segments.Insert(0, entityName);

            return flurlClient.Request(segments.ToArray());
        }

        protected async Task<T> PerformGet<T>(IFlurlRequest request) where T : class
        {
            return await request.GetJsonAsync<T>();
        }

        protected async Task<T> PerformPost<T>(IFlurlRequest request, object entity) where T : class
        {
            return await request
                .PostJsonAsync(entity)
                .ReceiveJson<T>();
        }

        protected async Task PerformPatch(IFlurlRequest request, IEnumerable<PatchOperation> patchOps)
        {
            await request.PatchJsonAsync(patchOps);
        }

        protected async Task<T> PerformPatch2<T>(IFlurlRequest request, IEnumerable<PatchOperation> patchOps) where T : class
        {
            return await request
                .PatchJsonAsync(patchOps)
                .ReceiveJson<T>();
        }

        protected async Task PerformPut<T>(IFlurlRequest request, T entity) where T : class
        {
            await request.PutJsonAsync(entity);
        }

        protected async Task PerformDelete(IFlurlRequest request)
        {
            await request.DeleteAsync();
        }

        private async Task HandleFlurlException(FlurlHttpException flurlEx)
        {
            var crlf = Environment.NewLine;
            var error = string.Empty;

            try
            {
                var responseText = await flurlEx.GetResponseStringAsync();
                if (string.IsNullOrEmpty(responseText))
                {
                    responseText = flurlEx.ToString();
                }
                responseText = responseText.Replace("at ", $"{crlf}at ");

                error = $"{flurlEx.Call.Request.Verb.Method} - {flurlEx.Call.Request.Url}{crlf}{crlf}{responseText}";
            }
            catch (Exception ex)
            {
                logger.Error($"Unable to get Flurl exception - {ex.Message}");
                return;
            }

            try
            {
                var navData = new ErrorViewNavData(error);
                logger.Error(navData.ErrorMessage);

                await Shell.Current.GoToAsync(nameof(ErrorView), true, navData.ToNavDataDictionary());
            }
            catch (Exception ex)
            {
                logger.Error($"Unable to show API exception - {ex.Message}{crlf}{error}");
            }
        }
    }
}
