namespace TatterFitness.Mobile.Models.Popups
{
    public class ContextMenuButtonMetadata
    {
        public string Title { get; set; }
        public bool IsEnabled { get; set; } = true;
        public int CommandParameter { get; set; }
        public DecisionPopupMetadata ConfirmDecisionMetadata { get; set; }
    }
}
