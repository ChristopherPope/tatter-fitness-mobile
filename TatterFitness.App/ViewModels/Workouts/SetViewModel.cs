using CommunityToolkit.Mvvm.ComponentModel;
using TatterFitness.Models.Enums;
using TatterFitness.Models.Workouts;

namespace TatterFitness.App.ViewModels.Workouts
{
    [INotifyPropertyChanged]
    public partial class SetViewModel
    {
        [ObservableProperty]
        private bool isCompleted;

        public double Weight
        {
            get => Set.Weight;
            set
            {
                SetProperty(Set.Weight, value, Set, (set, val) => set.Weight = val);
                Volume = Weight * Reps;
            }
        }

        public double Volume
        {
            get => Set.Volume;
            set => SetProperty(Set.Volume, value, Set, (set, val) => set.Volume = val);
        }

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

        public int Reps
        {
            get => Set.RepCount;
            set
            {
                SetProperty(Set.RepCount, value, Set, (set, val) => set.RepCount = val);
                Volume = Weight * Reps;
            }
        }

        public int DurationInSeconds
        {
            get => Set.DurationInSeconds;
            set => SetProperty(Set.DurationInSeconds, value, Set, (set, val) => set.DurationInSeconds = val);
        }

        [ObservableProperty]
        private bool doShowRepsAndWeight = false;

        [ObservableProperty]
        private bool doShowDurationAndWeight = false;

        [ObservableProperty]
        private bool doShowCardio = false;

        [ObservableProperty]
        private bool doShowRepsOnly = false;

        [ObservableProperty]
        private int setNumber;

        [ObservableProperty]
        private int totalSets;

        public WorkoutExerciseSet Set { get; set; }

        public SetViewModel(WorkoutExerciseSet set, ExerciseTypes exerciseType, int totalSets)
        {
            Set = set;
            SetNumber = set.SetNumber;
            TotalSets = totalSets;
            IsCompleted = set.Id > 0;
            Weight = set.Weight;
            Volume = set.Volume;
            MachineWatts = set.MachineWatts;
            MilesDistance = set.MilesDistance;
            MachineIntensity = set.MachineIntensity;
            MachineIncline = set.MachineIncline;
            MaxBpm = set.MaxBpm;
            Calories = set.Calories;
            Reps = set.RepCount;
            DurationInSeconds = set.DurationInSeconds;
            DoShowDurationAndWeight = exerciseType == ExerciseTypes.DurationAndWeight;
            DoShowRepsAndWeight = exerciseType == ExerciseTypes.RepsAndWeight;
            DoShowCardio = exerciseType == ExerciseTypes.Cardio;
            DoShowRepsOnly = exerciseType == ExerciseTypes.RepsOnly;
        }

        
    }
}
