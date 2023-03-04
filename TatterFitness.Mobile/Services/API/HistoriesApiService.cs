using Flurl.Http.Configuration;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;
using TatterFitness.Models;
using TatterFitness.Models.Exercises;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.Services.API
{
    public class HistoriesApiService : ApiServiceBase, IHistoriesApiService
    {
        public HistoriesApiService(ILoggingService logger, IFlurlClientFactory flurlClientFactory)
            : base("Histories", logger, flurlClientFactory)
        {
        }

        public async Task<IEnumerable<ExerciseHistory>> ReadExercise(int exerciseId)
        {
            return await PerformGet<IEnumerable<ExerciseHistory>>(CreateRequest("Exercise", exerciseId));
        }

        public async Task<WorkoutDateRange> ReadFirstAndLastWorkoutDates()
        {
            return await PerformGet<WorkoutDateRange>(CreateRequest("FirstAndLastWorkoutDates"));
        }

        public async Task<IEnumerable<EventDay>> ReadWorkoutEvents(DateTime inclusiveFrom, DateTime inclusiveTo)
        {
            var workoutRange = new WorkoutDateRange { InclusiveFrom = inclusiveFrom, InclusiveTo = inclusiveTo };
            return await PerformPost<IEnumerable<EventDay>>(CreateRequest("Workouts/Events"), workoutRange);
        }

        public async Task<IEnumerable<Workout>> ReadWorkouts(DateTime inclusiveFrom, DateTime inclusiveTo)
        {
            var workoutRange = new WorkoutDateRange { InclusiveFrom = inclusiveFrom, InclusiveTo = inclusiveTo };
            return await PerformPost<IEnumerable<Workout>>(CreateRequest("Workouts"), workoutRange);
        }
    }
}
