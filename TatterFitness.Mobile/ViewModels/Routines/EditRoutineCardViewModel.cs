using CommunityToolkit.Mvvm.ComponentModel;
using TatterFitness.Models.Exercises;
using TatterFitness.Mobile.Interfaces.Services;

namespace TatterFitness.Mobile.ViewModels.Routines
{
    public partial class EditRoutineCardViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string name;

        public RoutineExercise RoutineExercise { get; }
       
        public EditRoutineCardViewModel(ILoggingService logger, RoutineExercise routineExercise)
            : base(logger)
        {
            RoutineExercise = routineExercise;
            name = RoutineExercise.ExerciseName;
        }

        protected override Task PerformLoadViewData()
        {
            return Task.CompletedTask;
        }
    }
}
