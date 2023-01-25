using Flurl.Http.Configuration;
using TatterFitness.Models.Exercises;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Interfaces.Services.API;

namespace TatterFitness.App.Services.API
{
    public class ExercisesApiService : ApiServiceBase, IExercisesApiService
    {
        public ExercisesApiService(ILoggingService logger, IFlurlClientFactory flurlClientFactory)
            : base("Exercises", logger, flurlClientFactory)
        {
        }

        public async Task<IEnumerable<Exercise>> ReadAll()
        {
            return await PerformGet<IEnumerable<Exercise>>(CreateRequest());
        }

        public async Task<Exercise> Read(int exerciseId)
        {
            return await PerformGet<Exercise>(CreateRequest(exerciseId));
        }
    }
}
