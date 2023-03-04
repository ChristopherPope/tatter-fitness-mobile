using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.Interfaces.Services.API
{
    public interface IWorkoutExerciseModifiersApiService
    {
        Task<WorkoutExerciseModifier> Create(WorkoutExerciseModifier modifier);
        Task Delete(int workoutExerciseModifierId);
    }
}
