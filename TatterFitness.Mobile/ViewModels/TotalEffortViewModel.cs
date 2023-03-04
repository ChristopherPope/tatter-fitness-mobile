using CommunityToolkit.Mvvm.ComponentModel;
using TatterFitness.Mobile.Utils;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.ViewModels
{
    public partial class TotalEffortViewModel : ObservableObject
    {
        [ObservableProperty]
        private string totalRWVolume;

        [ObservableProperty]
        private string totalROReps;

        [ObservableProperty]
        private string totalDWDuration;

        [ObservableProperty]
        private string totalDWVolume;

        [ObservableProperty]
        private string totalCDuration;

        [ObservableProperty]
        private string totalCMiles;

        [ObservableProperty]
        private string averageBpm;

        public TotalEffortViewModel()
        {
            ShowTotalEffort(Enumerable.Empty<WorkoutExerciseSet>());
        }

        public void ShowTotalEffort(IEnumerable<WorkoutExerciseSet> sets)
        {
            var effortCalculator = new EffortCalculator();
            effortCalculator.Calculate(sets);

            var effortFormatter = new EffortFormatter();
            effortFormatter.FormatEffort(effortCalculator);

            TotalRWVolume = effortFormatter.RWVolume;
            TotalROReps = effortFormatter.ROReps;
            TotalDWDuration = effortFormatter.DWDuration;
            TotalDWVolume = effortFormatter.DWVolume;
            TotalCDuration = effortFormatter.CDuration;
            TotalCMiles = effortFormatter.CMiles;
            AverageBpm = effortFormatter.CBpm;
        }
    }
}
