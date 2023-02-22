using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TatterFitness.App.Controls.Popups;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Models;
using TatterFitness.App.Models.Popups;
using TatterFitness.App.Utils;
using TatterFitness.Mobile.ViewModels;
using TatterFitness.Models.Workouts;

namespace TatterFitness.App.ViewModels.WorkoutSnapshot
{
    public partial class WorkoutSnapshotCardViewModel : ViewModelBase
    {
        public WorkoutExercise WorkoutExercise { get; private set; }

        [ObservableProperty]
        public bool doShowModNames;

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
        public bool isCardioGridVisible;

        [ObservableProperty]
        public bool isRepsAndWeightGridVisible;

        [ObservableProperty]
        public bool isRepsOnlyGridVisible;

        [ObservableProperty]
        public bool isDurationAndWeightGridVisible;

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

            IsCardioGridVisible = WorkoutExercise.ExerciseType == TatterFitness.Models.Enums.ExerciseTypes.Cardio;
            IsRepsOnlyGridVisible = WorkoutExercise.ExerciseType == TatterFitness.Models.Enums.ExerciseTypes.RepsOnly;
            IsRepsAndWeightGridVisible = WorkoutExercise.ExerciseType == TatterFitness.Models.Enums.ExerciseTypes.RepsAndWeight;
            IsDurationAndWeightGridVisible = WorkoutExercise.ExerciseType == TatterFitness.Models.Enums.ExerciseTypes.DurationAndWeight;

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
