#if ANDROID
using Android.Util;
#endif

using TatterFitness.Mobile.Interfaces.Services;

namespace TatterFitness.Mobile.Services
{
    internal class LoggingService : ILoggingService
    {
#if ANDROID
        private readonly string infoTag = "TatterFitness-INFO";
        private readonly string errorTag = "TatterFitness-ERROR";
#endif

        public void Info(string message)
        {
#if ANDROID
            Log.Info(infoTag, message);
#endif
        }

        public void Error(string message)
        {
#if ANDROID
            Log.Error(errorTag, message);
#endif
        }
    }
}
