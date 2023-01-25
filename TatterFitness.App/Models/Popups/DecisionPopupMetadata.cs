namespace TatterFitness.App.Models.Popups
{
    public class DecisionPopupMetadata
    {
        public string Title { get; set; }
        public string Prompt { get; set; }
        public string YesButtonTitle { get; set; } = "Yes";
        public string NoButtonTitle { get; set; } = "No";
    }
}
