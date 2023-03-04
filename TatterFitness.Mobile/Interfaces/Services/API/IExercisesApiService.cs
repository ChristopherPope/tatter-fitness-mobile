using TatterFitness.Models.Exercises;

namespace TatterFitness.Mobile.Interfaces.Services.API
{
    public interface IExercisesApiService
    {
        Task<IEnumerable<Exercise>> ReadAll();
        Task<Exercise> Read(int exerciseId);
    }
}
