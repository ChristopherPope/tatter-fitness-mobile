using TatterFitness.Models;
using TatterFitness.Models.Exercises;

namespace TatterFitness.App.Interfaces.Services.API
{
    public interface IRoutinesApiService
    {
        Task<IEnumerable<Routine>> ReadAll();
        Task<IEnumerable<RoutineExercise>> ReadExercises(int routineId);
        Task Delete(int routineId);
        Task<Routine> Create(Routine newRoutine);
        Task Update(Routine routine);
        Task<Routine> Read(int routineId);
    }
}
