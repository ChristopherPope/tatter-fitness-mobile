using CommunityToolkit.Mvvm.Messaging.Messages;
using TatterFitness.Mobile.Messages.MessageArgs;

namespace TatterFitness.Mobile.Messages
{
    public class SetCountChangedMessage : ValueChangedMessage<SetCountChangedArgs>
    {
        public SetCountChangedMessage(SetCountChangedArgs args)
            : base(args)
        {
        }
    }
}
