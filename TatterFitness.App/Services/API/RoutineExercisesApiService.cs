using Flurl.Http.Configuration;
using TatterFitness.Models.Exercises;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Interfaces.Services.API;

namespace TatterFitness.App.Services.API
{
    public class RoutineExercisesApiService : ApiServiceBase, IRoutineExercisesApiService
    {
        public RoutineExercisesApiService(ILoggingService logger, IFlurlClientFactory flurlClientFactory)
            : base("RoutineExercises", logger, flurlClientFactory)
        {
        }

        public async Task<IEnumerable<RoutineExercise>> Create(int routineId, IEnumerable<int> exerciseIds)
        {
            return await PerformPost<IEnumerable<RoutineExercise>>(CreateRequest(routineId), exerciseIds);
        }

        public async Task Delete(int routineId, IEnumerable<int> exerciseIds)
        {
            var parms = exerciseIds.Select(re => $"{nameof(exerciseIds)}={re}");
            var request = CreateRequest(routineId);

            var url = request.Url.ToString();
            url += $"?{string.Join('&' , parms)}";

            request.Url = url;

            await PerformDelete(request);
        }
    }
}
