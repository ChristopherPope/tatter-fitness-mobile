using CommunityToolkit.Mvvm.ComponentModel;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.ViewModels.Workouts.WorkoutExercises
{
    public abstract partial class BaseSetViewModel : ObservableObject
    {
        protected readonly int exerciseId;

        [ObservableProperty]
        private bool isCompleted;

        [ObservableProperty]
        private int setNumber;

        [ObservableProperty]
        private int totalSets;

        [ObservableProperty]
        private int setId;

        public WorkoutExerciseSet Set { get; set; }

        public BaseSetViewModel(int exerciseId, WorkoutExerciseSet set, int totalSets)
        {
            this.exerciseId = exerciseId;
            Set = set;
            Refresh(totalSets);
        }

        public void Refresh(int totalSets)
        {
            SetNumber = Set.SetNumber;
            TotalSets = totalSets;
            IsCompleted = Set.Id > 0;
            SetId = Set.Id;
        }
    }
}
