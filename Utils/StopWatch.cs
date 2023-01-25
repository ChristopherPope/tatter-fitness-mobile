using TatterFitness.App.Interfaces.Services;

namespace TatterFitness.App.Utils
{
    public class StopWatch
    {
        private static StopWatch instance;
        private readonly ILoggingService logger;
        private DateTime time;
        private bool started = false;

        private StopWatch(ILoggingService logger)
        {
            this.logger = logger;
        }

        public static StopWatch GetInstance(ILoggingService logger)
        {
            if (instance == null)
            {
                instance = new StopWatch(logger);
            }

            return instance;
        }

        public void Start(string title)
        {
            time = DateTime.Now;
            started = true;
            logger.Info($"STOPWATCH started - {title}");
        }

        public void Lap(string lapName)
        {
            var now = DateTime.Now;
            if (!started)
            {
                logger.Error("STOPWATCH - Cannot lap stopwatch without starting it.");
                return;
            }

            var elapsed = now - time;
            time = now;
            logger.Info($"STOPWATCH - {lapName}: {elapsed}");
        }
    }
}
