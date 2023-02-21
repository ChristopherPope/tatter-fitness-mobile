using CommunityToolkit.Mvvm.Messaging.Messages;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.Messages
{
    public class SetMetricChangedMessage : ValueChangedMessage<WorkoutExerciseSet>
    {
        public SetMetricChangedMessage(WorkoutExerciseSet set)
            : base(set)
        {
        }
    }
}
