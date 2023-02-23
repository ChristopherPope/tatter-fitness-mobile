using TatterFitness.Models.Workouts;

namespace TatterFitness.App.ViewModels.Workouts.WorkoutExercises
{
    public partial class RepsOnlySetViewModel : BaseSetViewModel
    {
        public int Reps
        {
            get => Set.RepCount;
            set => SetProperty(Set.RepCount, value, Set, (set, val) => set.RepCount = val);
        }

        public RepsOnlySetViewModel(WorkoutExerciseSet set, int totalSets)
            : base(set, totalSets)
        {
            Reps = set.RepCount;
        }
    }
}
