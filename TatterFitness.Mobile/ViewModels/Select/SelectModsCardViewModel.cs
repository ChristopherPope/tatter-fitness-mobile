using CommunityToolkit.Mvvm.ComponentModel;
using TatterFitness.Models.Exercises;
using TatterFitness.Mobile.Interfaces.Services;

namespace TatterFitness.Mobile.ViewModels.Select
{
    public partial class SelectModsCardViewModel : ViewModelBase
    {
        [ObservableProperty]
        private bool isSelected = false;

        [ObservableProperty]
        public string name;

        public ExerciseModifier Modifier { get; private set; }

        public SelectModsCardViewModel(ILoggingService logger, ExerciseModifier modifier)
            : base(logger)
        {
            Modifier = modifier;
            Name = modifier.Name;
        }

        protected override Task PerformLoadViewData()
        {
            throw new NotImplementedException();
        }
    }
}
