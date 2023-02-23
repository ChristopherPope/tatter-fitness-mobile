using TatterFitness.Models.Workouts;

namespace TatterFitness.App.ViewModels.Workouts.WorkoutExercises
{
    public partial class CardioSetViewModel : BaseSetViewModel
    {
        public double MachineWatts
        {
            get => Set.MachineWatts;
            set => SetProperty(Set.MachineWatts, value, Set, (set, val) => set.MachineWatts = val);
        }

        public double MilesDistance
        {
            get => Set.MilesDistance;
            set => SetProperty(Set.MilesDistance, value, Set, (set, val) => set.MilesDistance = val);
        }

        public int MachineIntensity
        {
            get => Set.MachineIntensity;
            set => SetProperty(Set.MachineIntensity, value, Set, (set, val) => set.MachineIntensity = val);
        }

        public int MachineIncline
        {
            get => Set.MachineIncline;
            set => SetProperty(Set.MachineIncline, value, Set, (set, val) => set.MachineIncline = val);
        }

        public int MaxBpm
        {
            get => Set.MaxBpm;
            set => SetProperty(Set.MaxBpm, value, Set, (set, val) => set.MaxBpm = val);
        }

        public int Calories
        {
            get => Set.Calories;
            set => SetProperty(Set.Calories, value, Set, (set, val) => set.Calories = val);
        }

        public int DurationInSeconds
        {
            get => Set.DurationInSeconds;
            set => SetProperty(Set.DurationInSeconds, value, Set, (set, val) => set.DurationInSeconds = val);
        }

        public CardioSetViewModel(WorkoutExerciseSet set, int totalSets)
            : base(set, totalSets)
        {
            MachineWatts = set.MachineWatts;
            MilesDistance = set.MilesDistance;
            MachineIntensity = set.MachineIntensity;
            MachineIncline = set.MachineIncline;
            MaxBpm = set.MaxBpm;
            Calories = set.Calories;
            DurationInSeconds = set.DurationInSeconds;
        }
    }

}
