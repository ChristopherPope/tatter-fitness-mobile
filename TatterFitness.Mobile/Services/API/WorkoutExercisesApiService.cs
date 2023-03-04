using Flurl.Http.Configuration;
using TatterFitness.Models.Workouts;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;
using TatterFitness.Mobile.Enums;
using TatterFitness.Mobile.Models;

namespace TatterFitness.Mobile.Services.API
{
    public class WorkoutExercisesApiService : ApiServiceBase, IWorkoutExercisesApiService
    {
        public WorkoutExercisesApiService(ILoggingService logger, IFlurlClientFactory flurlClientFactory)
            : base("WorkoutExercises", logger, flurlClientFactory)
        {
        } 

        public async Task Update(WorkoutExercise workoutExercise)
        {
            var ops = new List<PatchOperation>
            {
                new PatchOperation(PatchOpCommand.Replace, $"/{nameof(WorkoutExercise.Notes)}", workoutExercise.Notes)
            };

            await PerformPatch(CreateRequest(workoutExercise.Id), ops);
        }

        public async Task<WorkoutExercise> Create(WorkoutExercise exercise)
        {
            return await PerformPost<WorkoutExercise>(CreateRequest(), exercise);
        }

        public async Task Delete(int workoutExerciseId)
        {
            await PerformDelete(CreateRequest(workoutExerciseId));
        }

        public async Task<IEnumerable<WorkoutExercise>> ReadMostRecent(IEnumerable<int> exerciseIds)
        {
            var parms = exerciseIds.Select(re => $"{nameof(exerciseIds)}={re}");
            var request = CreateRequest("MostRecent");

            var url = request.Url.ToString();
            url += $"?{string.Join('&', parms)}";
            request.Url = url;

            return await PerformGet<IEnumerable<WorkoutExercise>>(request);
        }
    }
}
