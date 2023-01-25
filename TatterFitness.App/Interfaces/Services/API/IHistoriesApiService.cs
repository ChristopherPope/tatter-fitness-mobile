using TatterFitness.Models;
using TatterFitness.Models.Exercises;
using TatterFitness.Models.Workouts;

namespace TatterFitness.App.Interfaces.Services.API
{
    public interface IHistoriesApiService
    {
        Task<IEnumerable<ExerciseHistory>> ReadExercise(int exerciseId);
        Task<IEnumerable<EventDay>> ReadWorkoutEvents(DateTime inclusiveFrom, DateTime inclusiveTo);
        Task<IEnumerable<Workout>> ReadWorkouts(DateTime inclusiveFrom, DateTime inclusiveTo);
        Task<WorkoutDateRange> ReadFirstAndLastWorkoutDates();
    }
}
