using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.Interfaces.Services.API
{
    public interface IWorkoutsApiService
    {
        Task<Workout> Create(Workout workout);
        Task Update(Workout workout);
        Task<Workout> Read(int workoutId);
    }
}
