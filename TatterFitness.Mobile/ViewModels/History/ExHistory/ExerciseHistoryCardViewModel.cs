using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TatterFitness.Mobile.Controls.Popups;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Models;
using TatterFitness.Mobile.Models.Popups;
using TatterFitness.Mobile.Utils;
using TatterFitness.Models.Enums;
using TatterFitness.Models.Exercises;

namespace TatterFitness.Mobile.ViewModels.History.ExHistory
{
    public partial class ExerciseHistoryCardViewModel : ViewModelBase
    {
        private readonly ExerciseHistory exerciseHistory;

        public ExerciseHistory ExerciseHistory => exerciseHistory;

        #region Set Metrics Properties
        [ObservableProperty]
        private string rWVolume;

        [ObservableProperty]
        private string dWVolume;

        [ObservableProperty]
        private string rOReps;

        [ObservableProperty]
        private string dWDuration;

        [ObservableProperty]
        private string dwVolume;

        [ObservableProperty]
        private string cDuration;

        [ObservableProperty]
        private string cMiles;

        [ObservableProperty]
        private string averageBpm;

        [ObservableProperty]
        private ExerciseTypes exerciseType;
        #endregion

        [ObservableProperty]
        private bool doShowModNames = false;

        [ObservableProperty]
        private string modNames;

        [ObservableProperty]
        private string workoutName;

        [ObservableProperty]
        private string workoutDate;

        [ObservableProperty]
        private ObservableCollection<SetSummary> setSummaries = new();

        [ObservableProperty]
        private bool doShowNotesButton = false;

        public ExerciseHistoryCardViewModel(ILoggingService logger, ExerciseHistory exerciseHistory)
            : base(logger)
        {
            this.exerciseHistory = exerciseHistory;
        }

        protected override Task PerformLoadViewData()
        {
            var setSummarizer = new SetSummariesMaker();
            foreach (var summary in setSummarizer.MakeSummaries(exerciseHistory.Sets, exerciseHistory.ExerciseType))
            {
                SetSummaries.Add(new SetSummary { Summary = summary });
            }

            WorkoutName = $"{exerciseHistory.WorkoutName}";
            WorkoutDate = exerciseHistory.WorkoutDate.ToString("MM/dd/yy");
            ModNames = String.Join(", ", exerciseHistory.Mods.Select(m => m.ModifierName).ToArray());
            DoShowModNames = !string.IsNullOrEmpty(ModNames);
            DoShowNotesButton = !string.IsNullOrEmpty(exerciseHistory.Notes);
            ExerciseType = exerciseHistory.ExerciseType;

            CalculateMetricsEffort();
            return Task.CompletedTask;
        }

        private void CalculateMetricsEffort()
        {
            var calculator = new EffortCalculator();
            calculator.Calculate(exerciseHistory.Sets);

            var formatter = new EffortFormatter();
            formatter.FormatEffort(calculator);

            RWVolume = formatter.RWVolume;
            DWVolume = formatter.DWVolume;
            DWDuration = formatter.DWDuration;
            ROReps = formatter.ROReps;
            CDuration = formatter.CDuration;
            CMiles = formatter.CMiles;
            AverageBpm = formatter.CBpm;
        }

        [RelayCommand]
        private async Task ShowNotes()
        {
            try
            {
                var metadata = new NotesPopupMetadata
                {
                    Notes = exerciseHistory.Notes,
                    ExerciseName = exerciseHistory.ExerciseName
                };
                var notesPopup = new NotesPopup(metadata);
                var notes = await Shell.Current.ShowPopupAsync(notesPopup) as string;
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }
    }
}
