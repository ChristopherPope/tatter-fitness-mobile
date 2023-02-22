using CommunityToolkit.Mvvm.Messaging.Messages;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.Messages
{
    public class SetCompletedMessage : ValueChangedMessage<WorkoutExerciseSet>
    {
        public SetCompletedMessage(WorkoutExerciseSet set)
            : base(set)
        {
        }
    }
}
