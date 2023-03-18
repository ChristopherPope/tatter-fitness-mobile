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
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.ViewModels.WorkoutSnapshot
{
    public partial class WorkoutSnapshotCardViewModel : ViewModelBase
    {
        public WorkoutExercise WorkoutExercise { get; private set; }

        [ObservableProperty]
        public bool doShowModNames;

        [ObservableProperty]
        private string ftoInfo;

        [ObservableProperty]
        private string modNames;

        [ObservableProperty]
        private int summaryCount;

        [ObservableProperty]
        private string summary;

        [ObservableProperty]
        private string exerciseName;

        [ObservableProperty]
        private bool doShowNotesButton = false;

        [ObservableProperty]
        public ExerciseTypes exerciseType;

        [ObservableProperty]
        private ObservableCollection<SetSummary> setSummaries = new();

        public TotalEffortViewModel TotalEffort { get; private set; }

        public WorkoutSnapshotCardViewModel(ILoggingService logger, WorkoutExercise workoutExercise) : base(logger)
        {
            WorkoutExercise = workoutExercise;
            TotalEffort = new TotalEffortViewModel();
        }

        protected override Task PerformLoadViewData()
        {
            var setSummarizer = new SetSummariesMaker();
            var summaries = setSummarizer.MakeSummaries(WorkoutExercise.Sets, WorkoutExercise.ExerciseType);
            foreach (var summary in summaries)
            {
                SetSummaries.Add(new SetSummary { Summary = summary });
            }

            ExerciseName = WorkoutExercise.ExerciseName;
            MakeModNames();
            DoShowNotesButton = !string.IsNullOrEmpty(WorkoutExercise.Notes);
            TotalEffort.ShowTotalEffort(WorkoutExercise.Sets);
            ExerciseType = WorkoutExercise.ExerciseType;

            if (WorkoutExercise.FtoWeekNumber > 0 || WorkoutExercise.FtoTrainingMax > 0)
            {
                FtoInfo = $"531 - Week Number: {WorkoutExercise.FtoWeekNumber}, Training Max: {WorkoutExercise.FtoTrainingMax:#,0} lbs.";
            }
            return Task.CompletedTask;
        }

        private void MakeModNames()
        {
            WorkoutExercise.Mods
                .Sort((mod1, mod2) => mod1.ModifierSequence.CompareTo(mod2.ModifierSequence));

            var modNames = WorkoutExercise.Mods
                .Select(e => e.ModifierName).ToArray();
            ModNames = String.Join(", ", modNames);
            DoShowModNames = !string.IsNullOrEmpty(ModNames);
        }

        [RelayCommand]
        private async Task ShowNotes()
        {
            try
            {
                var metadata = new NotesPopupMetadata
                {
                    Notes = WorkoutExercise.Notes,
                    ExerciseName = WorkoutExercise.ExerciseName
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
