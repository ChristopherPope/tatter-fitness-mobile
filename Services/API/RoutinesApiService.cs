using Flurl.Http.Configuration;
using TatterFitness.Models;
using TatterFitness.Models.Exercises;
using TatterFitness.App.Enums;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Interfaces.Services.API;
using TatterFitness.App.Models;

namespace TatterFitness.App.Services.API
{
    public class RoutinesApiService : ApiServiceBase, IRoutinesApiService
    {
        public RoutinesApiService(ILoggingService logger, IFlurlClientFactory flurlClientFactory)
            : base("Routines", logger, flurlClientFactory)
        {
        }
        
        public async Task<Routine> Read(int routineId)
        {
            return await PerformGet<Routine>(CreateRequest(routineId));
        }

        public Task<Routine> Create(Routine newRoutine)
        {
            return PerformPost<Routine>(CreateRequest(), newRoutine);
        }

        public async Task Delete(int routineId)
        {
            await PerformDelete(CreateRequest(routineId));
        }

        public async Task<IEnumerable<Routine>> ReadAll()
        {
            return await PerformGet<IEnumerable<Routine>>(CreateRequest());
        }

        public async Task<IEnumerable<RoutineExercise>> ReadExercises(int routineId)
        {
            var routine = await PerformGet<Routine>(CreateRequest(routineId));

            return routine.Exercises;
        }

        public async Task Update(Routine routine)
        {
            var ops = new List<PatchOperation>
            {
                new PatchOperation(PatchOpCommand.Replace, $"/{nameof(Routine.Name)}", routine.Name)
            };

            await PerformPatch(CreateRequest(routine.Id), ops);
        }
    }
}
