using CommunityToolkit.Mvvm.Messaging.Messages;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.Messages
{
    public class ExerciseModsChangedMessage : ValueChangedMessage<WorkoutExercise>
    {
        public ExerciseModsChangedMessage(WorkoutExercise workoutExercise)
            : base(workoutExercise)
        {
        }
    }
}
