using Flurl.Http.Configuration;
using TatterFitness.Models.Exercises;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Interfaces.Services.API;

namespace TatterFitness.App.Services.API
{
    internal class ExerciseModifiersApiService : ApiServiceBase, IExerciseModifiersApiService
    {
        public ExerciseModifiersApiService(ILoggingService logger, IFlurlClientFactory flurlClientFactory)
            : base("ExerciseModifiers", logger, flurlClientFactory)
        {
        }

        public async Task<IEnumerable<ExerciseModifier>> ReadAll()
        {
            return await PerformGet<IEnumerable<ExerciseModifier>>(CreateRequest());
        }
    }
}
