using CommunityToolkit.Mvvm.Messaging.Messages;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.Messages
{
    public class SetDeletedMessage : ValueChangedMessage<WorkoutExerciseSet>
    {
        public SetDeletedMessage(WorkoutExerciseSet set)
            : base(set)
        {
        }
    }
}
