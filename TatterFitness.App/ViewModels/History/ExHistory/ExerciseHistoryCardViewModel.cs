using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TatterFitness.App.Controls.Popups;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Models.Popups;
using TatterFitness.Models.Enums;
using TatterFitness.Models.Exercises;
using TatterFitness.Models.Workouts;
using System.Collections.ObjectModel;
using TatterFitness.App.Utils;
using TatterFitness.App.Models;

namespace TatterFitness.App.ViewModels.History.ExHistory
{
    public partial class ExerciseHistoryCardViewModel : ViewModelBase
    {
        private readonly ExerciseHistory exerciseHistory;

        public ExerciseHistory ExerciseHistory => exerciseHistory;

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
                setSummaries.Add(new SetSummary {  Summary = summary });
            }

            WorkoutName = $"{exerciseHistory.WorkoutName}";
            WorkoutDate = exerciseHistory.WorkoutDate.ToString("MM/dd/yy");
            ModNames = String.Join(", ", exerciseHistory.Mods.Select(m => m.ModifierName).ToArray());
            DoShowModNames = !string.IsNullOrEmpty(ModNames);
            DoShowNotesButton = !string.IsNullOrEmpty(exerciseHistory.Notes);

            return Task.CompletedTask;
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
