using CommunityToolkit.Mvvm.Messaging.Messages;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.Messages
{
    public class CompletedSetMetricsChangedMessage : ValueChangedMessage<WorkoutExerciseSet>
    {
        public CompletedSetMetricsChangedMessage(WorkoutExerciseSet set)
            : base(set)
        {
        }
    }
}
