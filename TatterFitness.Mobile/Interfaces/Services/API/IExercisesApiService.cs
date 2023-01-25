using TatterFitness.Models.Exercises;

namespace TatterFitness.App.Interfaces.Services.API
{
    public interface IExercisesApiService
    {
        Task<IEnumerable<Exercise>> ReadAll();
        Task<Exercise> Read(int exerciseId);
    }
}
