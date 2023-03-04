using Flurl.Http.Configuration;
using TatterFitness.Models.Exercises;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;

namespace TatterFitness.Mobile.Services.API
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
