
using TatterFitness.Models.Exercises;

namespace TatterFitness.Mobile.Interfaces.Services.API
{
    public interface IExerciseModifiersApiService
    {
        Task<IEnumerable<ExerciseModifier>> ReadAll();
    }
}
