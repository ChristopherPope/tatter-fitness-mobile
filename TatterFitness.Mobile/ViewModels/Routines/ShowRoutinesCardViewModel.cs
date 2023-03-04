using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Models;

namespace TatterFitness.Mobile.ViewModels.Routines
{
    public partial class ShowRoutinesCardViewModel : ViewModelBase
    {
        public Routine Routine { get; private set; }

        public string RoutineName
        {
            get => Routine.Name;
            set => SetProperty(Routine.Name, value, Routine, (routine, val) => routine.Name = val);
        }


        public ShowRoutinesCardViewModel(ILoggingService logger, Routine routine)
            : base(logger)
        {
            Routine = routine;
        }

        protected override Task PerformLoadViewData()
        {
            return Task.CompletedTask;
        }
    }
}
