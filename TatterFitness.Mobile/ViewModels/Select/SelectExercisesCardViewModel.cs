using CommunityToolkit.Mvvm.ComponentModel;
using TatterFitness.Models.Exercises;
using TatterFitness.App.Interfaces.Services;

namespace TatterFitness.App.ViewModels.Select
{
    public partial class SelectExercisesCardViewModel : ViewModelBase
    {
        [ObservableProperty]
        private bool isSelected = false;

        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private bool isRequired;

        [ObservableProperty]
        private Exercise exercise;

        public SelectExercisesCardViewModel(ILoggingService logger, Exercise exercise)
            : base(logger)
        {
            Exercise = exercise;
            name = exercise.Name;
        }

        protected override Task PerformLoadViewData()
        {
            throw new NotImplementedException();
        }
    }
}
