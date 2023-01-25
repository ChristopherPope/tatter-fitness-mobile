using TatterFitness.Models.Enums;
using TatterFitness.Models.Workouts;

namespace TatterFitness.App.Utils
{
    public class SetSummariesMaker
    {

        public IEnumerable<string> MakeSummaries(IEnumerable<WorkoutExerciseSet> sets, ExerciseTypes exerciseType)
        {
            var summaries = new List<string>();
            var setGroups = sets.GroupBy(s => s.Key);
            foreach (var setGroup in setGroups)
            {
                summaries.Add(MakeSetSummary(setGroup, exerciseType));
            }

            return summaries;
        }

        private string MakeSetSummary(IEnumerable<WorkoutExerciseSet> setGroup, ExerciseTypes exerciseType)
        {
            var firstSet = setGroup.First();
            return exerciseType switch
            {
                ExerciseTypes.RepsAndWeight => $"{setGroup.Count()} x {firstSet.RepCount} @{firstSet.Weight} lbs.",
                ExerciseTypes.RepsOnly => $"{setGroup.Count()} x {firstSet.RepCount}",
                ExerciseTypes.DurationAndWeight => $"{setGroup.Count()} x {TimeSpan.FromSeconds(firstSet.DurationInSeconds)} @{firstSet.Weight} lbs.",
                ExerciseTypes.Cardio => $"{setGroup.Count()} x {TimeSpan.FromSeconds(firstSet.DurationInSeconds)} @{firstSet.MilesDistance} mi.",
                _ => String.Empty,
            };
        }
    }
}
