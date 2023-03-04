using Flurl.Http.Configuration;
using TatterFitness.Models;
using TatterFitness.Models.Workouts;
using TatterFitness.Mobile.Enums;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;
using TatterFitness.Mobile.Models;

namespace TatterFitness.Mobile.Services.API
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
