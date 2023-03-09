using CommunityToolkit.Mvvm.Input;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.ViewModels.Workouts.WorkoutExercises
{
    public partial class RepsAndWeightSetViewModel : BaseSetViewModel
    {
        public IAsyncRelayCommand<int> UpdateTheMetricCommand { get; private set; }

        public Task UpdateTheMetric(int setId)
        {
            return Task.CompletedTask;
        }


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

        public int Reps
        {
            get => Set.RepCount;
            set
            {
                SetProperty(Set.RepCount, value, Set, (set, val) => set.RepCount = val);
                Volume = Weight * Reps;
            }
        }
        public RepsAndWeightSetViewModel(int exerciseId, WorkoutExerciseSet set, int totalSets)
            : base(exerciseId, set, totalSets)
        {
            Weight = set.Weight;
            Volume = set.Volume;
            Reps = set.RepCount;

            UpdateTheMetricCommand = new AsyncRelayCommand<int>(UpdateTheMetric);
        }

    }
}
