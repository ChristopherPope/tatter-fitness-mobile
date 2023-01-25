using TatterFitness.Models.Workouts;

namespace TatterFitness.App.Interfaces.Services.API
{
    public interface IWorkoutExerciseSetsApiService
    {
        Task<WorkoutExerciseSet> Create(WorkoutExerciseSet set);
        Task<WorkoutExerciseSet> Update(WorkoutExerciseSet set);
        Task Delete(int workoutExerciseSetId);
    }
}
