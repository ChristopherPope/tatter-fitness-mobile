using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.ViewModels.Workouts.WorkoutExercises
{
    public partial class DurationAndWeightSetViewModel : BaseSetViewModel
    {
        public double Weight
        {
            get => Set.Weight;
            set => SetProperty(Set.Weight, value, Set, (set, val) => set.Weight = val);
        }

        public int DurationInSeconds
        {
            get => Set.DurationInSeconds;
            set => SetProperty(Set.DurationInSeconds, value, Set, (set, val) => set.DurationInSeconds = val);
        }

        public DurationAndWeightSetViewModel(int exerciseId, WorkoutExerciseSet set, int totalSets)
            : base(exerciseId, set, totalSets)
        {
            Weight = set.Weight;
            DurationInSeconds = set.DurationInSeconds;
        }
    }
}
