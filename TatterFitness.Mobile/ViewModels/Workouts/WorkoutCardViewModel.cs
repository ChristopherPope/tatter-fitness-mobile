using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Interfaces.Services.API;
using TatterFitness.App.Interfaces.Services.SelectorModals;
using TatterFitness.Mobile.Messages;
using TatterFitness.Mobile.ViewModels;
using TatterFitness.Models.Exercises;
using TatterFitness.Models.Workouts;

namespace TatterFitness.App.ViewModels.Workouts
{
    public partial class WorkoutCardViewModel :
        ViewModelBase,
        IRecipient<CompletedSetMetricsChangedMessage>,
        IRecipient<SetAddedMessage>,
        IRecipient<SetDeletedMessage>,
        IRecipient<ExerciseModsChangedMessage>,
        IRecipient<SetCompletedMessage>
    {
        private readonly IWorkoutExerciseModifiersApiService modsSvc;
        private readonly IMapper mapper;
        private readonly IModsSelectorModal modsSelectorModal;

        [ObservableProperty]
        private TotalEffortViewModel totalEffort;

        [ObservableProperty]
        private WorkoutExercise workoutExercise;

        [ObservableProperty]
        public bool doShowModNames;

        [ObservableProperty]
        public bool isCardioGridVisible;

        [ObservableProperty]
        public bool isRepsAndWeightGridVisible;

        [ObservableProperty]
        public bool isRepsOnlyGridVisible;

        [ObservableProperty]
        public bool isDurationAndWeightGridVisible;

        [ObservableProperty]
        private string modNames;

        [ObservableProperty]
        private string exerciseName;

        [ObservableProperty]
        private string setsCompletedTitle = string.Empty;

        [ObservableProperty]
        private double setsCompletedPercent = 0;

        private IEnumerable<WorkoutExerciseSet> CompletedSets
        {
            get
            {
                return WorkoutExercise.Sets.Where(s => s.Id > 0);
            }
        }

        public WorkoutCardViewModel(ILoggingService logger, IModsSelectorModal modsSelectorModal, IWorkoutExerciseModifiersApiService modsSvc, IMapper mapper, WorkoutExercise workoutExercise)
            : base(logger)
        {
            WorkoutExercise = workoutExercise;
            totalEffort = new TotalEffortViewModel();
            this.mapper = mapper;
            this.modsSelectorModal = modsSelectorModal;
            this.modsSvc = modsSvc;

            WeakReferenceMessenger.Default.Register(this as IRecipient<ExerciseModsChangedMessage>);
            WeakReferenceMessenger.Default.Register(this as IRecipient<CompletedSetMetricsChangedMessage>);
            WeakReferenceMessenger.Default.Register(this as IRecipient<SetAddedMessage>);
            WeakReferenceMessenger.Default.Register(this as IRecipient<SetDeletedMessage>);
            WeakReferenceMessenger.Default.Register(this as IRecipient<SetCompletedMessage>);
        }

        public bool HasCompletedSets()
        {
            return CompletedSets.Any();
        }

        public async Task SelectMods()
        {
            await modsSelectorModal.ShowModal(WorkoutExercise.Mods.Select(m => m.ExerciseModifierId), OnSelectModsModalClosed);
        }

        public void Receive(ExerciseModsChangedMessage message)
        {
            FormModNames();
        }

        public void Receive(CompletedSetMetricsChangedMessage message)
        {
            UpdateMetrics();
        }

        public void Receive(SetCompletedMessage message)
        {
            UpdateMetrics();
        }

        public void Receive(SetAddedMessage message)
        {
            UpdateMetrics();
        }

        public void Receive(SetDeletedMessage message)
        {
            UpdateMetrics();
        }

        protected override Task PerformLoadViewData()
        {
            CalculateSetsCompleted();
            ExerciseName = WorkoutExercise.ExerciseName;
            FormModNames();

            IsCardioGridVisible = WorkoutExercise.ExerciseType == TatterFitness.Models.Enums.ExerciseTypes.Cardio;
            IsRepsOnlyGridVisible = WorkoutExercise.ExerciseType == TatterFitness.Models.Enums.ExerciseTypes.RepsOnly;
            IsRepsAndWeightGridVisible = WorkoutExercise.ExerciseType == TatterFitness.Models.Enums.ExerciseTypes.RepsAndWeight;
            IsDurationAndWeightGridVisible = WorkoutExercise.ExerciseType == TatterFitness.Models.Enums.ExerciseTypes.DurationAndWeight;

            return Task.CompletedTask;
        }

        private async Task OnSelectModsModalClosed(IEnumerable<ExerciseModifier> modsToAdd, IEnumerable<ExerciseModifier> modsToRemove)
        {
            try
            {
                await AddModifiers(modsToAdd);
                await RemoveModifiers(modsToRemove);

                WeakReferenceMessenger.Default.Send(new ExerciseModsChangedMessage(WorkoutExercise));
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex);
            }
        }

        private void FormModNames()
        {
            WorkoutExercise.Mods
                .Sort((mod1, mod2) => mod1.ModifierSequence.CompareTo(mod2.ModifierSequence));

            var modNames = WorkoutExercise.Mods
                .Select(e => e.ModifierName).ToArray();
            ModNames = String.Join(", ", modNames);
            DoShowModNames = !string.IsNullOrEmpty(ModNames);
        }

        private void CalculateSetsCompleted()
        {
            var setsCompleted = CompletedSets.Count();
            SetsCompletedTitle = $"{setsCompleted} of {WorkoutExercise.Sets.Count} Sets Completed";

            SetsCompletedPercent = Convert.ToDouble(setsCompleted) / Convert.ToDouble(WorkoutExercise.Sets.Count);
        }

        private async Task AddModifiers(IEnumerable<ExerciseModifier> modifiers)
        {
            var workoutExModifiers = mapper.Map<List<WorkoutExerciseModifier>>(modifiers.ToList());

            if (WorkoutExercise.Id > 0)
            {
                foreach (var workoutExMod in workoutExModifiers)
                {
                    workoutExMod.WorkoutExerciseId = WorkoutExercise.Id;
                    var createdMod = await modsSvc.Create(workoutExMod);
                    workoutExMod.Id = createdMod.Id;
                }
            }
            WorkoutExercise.Mods.AddRange(workoutExModifiers);
            FormModNames();
        }

        private async Task RemoveModifiers(IEnumerable<ExerciseModifier> modifiers)
        {
            foreach (var modifier in modifiers)
            {
                var workoutExMod = WorkoutExercise.Mods
                    .Where(m => m.ExerciseModifierId == modifier.Id)
                    .First();

                WorkoutExercise.Mods.Remove(workoutExMod);
                if (WorkoutExercise.Id > 0)
                {
                    await modsSvc.Delete(workoutExMod.Id);
                }
            }

            FormModNames();
        }

        private void UpdateMetrics()
        {
            totalEffort.ShowTotalEffort(WorkoutExercise.Sets);
            CalculateSetsCompleted();
        }
    }
}
