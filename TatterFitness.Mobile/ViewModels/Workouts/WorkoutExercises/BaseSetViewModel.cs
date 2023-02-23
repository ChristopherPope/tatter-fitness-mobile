using CommunityToolkit.Mvvm.ComponentModel;
using TatterFitness.Models.Workouts;

namespace TatterFitness.App.ViewModels.Workouts.WorkoutExercises
{
    [INotifyPropertyChanged]
    public abstract partial class BaseSetViewModel
    {
        [ObservableProperty]
        private bool isCompleted;

        [ObservableProperty]
        private int setNumber;

        [ObservableProperty]
        private int totalSets;

        public WorkoutExerciseSet Set { get; set; }

        public BaseSetViewModel(WorkoutExerciseSet set, int totalSets)
        {
            Set = set;
            SetNumber = set.SetNumber;
            TotalSets = totalSets;
            IsCompleted = set.Id > 0;
        }
    }
}
