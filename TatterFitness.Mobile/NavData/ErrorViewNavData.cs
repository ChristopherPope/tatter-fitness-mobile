namespace TatterFitness.Mobile.NavData
{
    public class ErrorViewNavData : NavDataBase
    {
        public string ErrorMessage { get; private set; }

        public ErrorViewNavData(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
