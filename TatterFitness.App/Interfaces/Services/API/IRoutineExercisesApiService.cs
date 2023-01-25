using TatterFitness.Models.Exercises;

namespace TatterFitness.App.Interfaces.Services.API
{
    public interface IRoutineExercisesApiService
    {
        Task Delete(int routineId, IEnumerable<int> exerciseIds);
        Task<IEnumerable<RoutineExercise>> Create(int routineId, IEnumerable<int> exerciseIds);
    }
}
