
using TatterFitness.Models.Exercises;

namespace TatterFitness.App.Interfaces.Services.API
{
    public interface IExerciseModifiersApiService
    {
        Task<IEnumerable<ExerciseModifier>> ReadAll();
    }
}
