namespace TatterFitness.App.Interfaces.Services
{
    public interface ILoggingService
    {
        void Info(string message);
        void Error(string message);
    }
}
