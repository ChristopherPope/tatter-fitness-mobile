using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.Interfaces.Services.API
{
    public interface IWorkoutExercisesApiService
    {
        Task<WorkoutExercise> Create(WorkoutExercise exercise);
        Task Delete(int workoutExerciseId);
        Task<IEnumerable<WorkoutExercise>> ReadMostRecent(IEnumerable<int> exerciseIds);
        Task Update(WorkoutExercise workoutExercise);
    }
}
