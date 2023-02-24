using CommunityToolkit.Mvvm.Messaging.Messages;
using TatterFitness.Mobile.Messages.MessageArgs;

namespace TatterFitness.Mobile.Messages
{
    public class SetDeletedMessage : ValueChangedMessage<SetDeletedArgs>
    {
        public SetDeletedMessage(SetDeletedArgs set)
            : base(set)
        {
        }
    }
}
