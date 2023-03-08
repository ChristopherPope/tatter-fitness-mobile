using CommunityToolkit.Mvvm.Messaging.Messages;

namespace TatterFitness.Mobile.Messages
{
    public class CompletedSetMetricsChangedMessage : ValueChangedMessage<int>
    {
        public CompletedSetMetricsChangedMessage(int setId)
            : base(setId)
        {
        }
    }
}
