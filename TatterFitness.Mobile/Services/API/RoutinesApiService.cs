using Flurl.Http.Configuration;
using TatterFitness.Models;
using TatterFitness.Models.Exercises;
using TatterFitness.Mobile.Enums;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;
using TatterFitness.Mobile.Models;

namespace TatterFitness.Mobile.Services.API
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
