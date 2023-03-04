using Flurl.Http.Configuration;
using TatterFitness.Models.Workouts;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;

namespace TatterFitness.Mobile.Services.API
{
    public class WorkoutExerciseModifiersApiService : ApiServiceBase, IWorkoutExerciseModifiersApiService
    {
        public WorkoutExerciseModifiersApiService(ILoggingService logger, IFlurlClientFactory flurlClientFactory) 
            : base("WorkoutExerciseModifiers", logger, flurlClientFactory)
        {
        }

        public async Task<WorkoutExerciseModifier> Create(WorkoutExerciseModifier modifier)
        {
            return await PerformPost<WorkoutExerciseModifier>(CreateRequest(), modifier);
        }

        public async Task Delete(int workoutExerciseModifierId)
        {
            await PerformDelete(CreateRequest(workoutExerciseModifierId));
        }
    }
}
