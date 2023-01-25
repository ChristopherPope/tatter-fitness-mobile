using Flurl.Http.Configuration;
using TatterFitness.Models;
using TatterFitness.Models.Workouts;
using TatterFitness.App.Enums;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Interfaces.Services.API;
using TatterFitness.App.Models;

namespace TatterFitness.App.Services.API
{
    public class WorkoutsApiService : ApiServiceBase, IWorkoutsApiService
    {
        public WorkoutsApiService(ILoggingService logger, IFlurlClientFactory flurlClientFactory)
            : base("Workouts", logger, flurlClientFactory)
        {
        }

        public async Task<Workout> Read(int workoutId)
        {
            return await PerformGet<Workout>(CreateRequest(workoutId));
        }

        public async Task<Workout> Create(Workout workout)
        {
            return await PerformPost<Workout>(CreateRequest(), workout);
        }

        public async Task Update(Workout workout)
        {
            var ops = new List<PatchOperation>
            {
                new PatchOperation(PatchOpCommand.Replace, $"/{nameof(Workout.Name)}", workout.Name)
            };

            await PerformPatch(CreateRequest(workout.Id), ops);
        }
    }
}
