using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.ViewModels.Workouts.WorkoutExercises
{
    public partial class RepsOnlySetViewModel : BaseSetViewModel
    {
        public int Reps
        {
            get => Set.RepCount;
            set => SetProperty(Set.RepCount, value, Set, (set, val) => set.RepCount = val);
        }

        public RepsOnlySetViewModel(int exerciseId, WorkoutExerciseSet set, int totalSets)
            : base(exerciseId, set, totalSets)
        {
            Reps = set.RepCount;
        }
    }
}
