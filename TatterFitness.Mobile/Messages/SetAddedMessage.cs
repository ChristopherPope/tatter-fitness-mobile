using CommunityToolkit.Mvvm.Messaging.Messages;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.Messages
{
    public class SetAddedMessage : ValueChangedMessage<WorkoutExerciseSet>
    {
        public SetAddedMessage(WorkoutExerciseSet set)
            : base(set)
        {
        }
    }
}
