using TatterFitness.Models.Enums;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.Utils
{
    public class EffortCalculator
    {
        public double RWVolume { get; private set; }

        public int ROReps { get; private set; }

        public int DWDurationInSeconds { get; private set; }

        public double DWVolume { get; private set; }

        public double CMiles { get; private set; }

        public int CDurationInSeconds { get; private set; }

        public int CBpm { get; private set; }

        private IEnumerable<WorkoutExerciseSet> sets = Enumerable.Empty<WorkoutExerciseSet>();

        public void Calculate(IEnumerable<WorkoutExerciseSet> sets)
        {
            this.sets = sets;
            CalculateRepsAndWeightVolume();
            CalculateRepsOnly();
            CalculateDurationAndWeightVolume();
            CalculateDurationAndWeightDurationInSeconds();
            CalculateCardioMiles();
            CalculateCardioDurationInSeconds();
            CalculateCardioBpm();
        }

        private void CalculateRepsAndWeightVolume()
        {
            RWVolume = GetSets(ExerciseTypes.RepsAndWeight).Sum(s => s.Weight * s.RepCount);
        }

        private void CalculateRepsOnly()
        {
            ROReps = GetSets(ExerciseTypes.RepsOnly).Sum(s => s.RepCount);
        }

        private void CalculateDurationAndWeightVolume()
        {
            DWVolume = GetSets(ExerciseTypes.DurationAndWeight).Sum(s => s.Weight);
        }

        private void CalculateDurationAndWeightDurationInSeconds()
        {
            DWDurationInSeconds = GetSets(ExerciseTypes.DurationAndWeight).Sum(s => s.DurationInSeconds);
        }

        private void CalculateCardioMiles()
        {
            CMiles = GetSets(ExerciseTypes.Cardio).Sum(s => s.MilesDistance);
        }

        private void CalculateCardioDurationInSeconds()
        {
            CDurationInSeconds = GetSets(ExerciseTypes.Cardio).Sum(s => s.DurationInSeconds);
        }

        private void CalculateCardioBpm()
        {
            var cardioSets = GetSets(ExerciseTypes.Cardio);
            var bpm = cardioSets.Sum(s => s.MaxBpm);

            if (cardioSets.Any())
            {
                CBpm = bpm / cardioSets.Count();
            }
            else
            {
                CBpm = 0;
            }
        }

        private IEnumerable<WorkoutExerciseSet> GetSets(ExerciseTypes exerciseType)
        {
            return sets
                .Where(s => s.ExerciseType == exerciseType)
                .Where(s => s.Id > 0)
                .ToList();
        }
    }
}
