using AutoMapper;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Interfaces.Services.API;
using TatterFitness.App.Interfaces.Services.SelectorModals;
using TatterFitness.Mobile.ViewModels;
using TatterFitness.Models.Workouts;

namespace TatterFitness.App.ViewModels.Workouts.WorkoutExercises
{
    public partial class DurationAndWeightWorkoutExerciseViewModel : BaseWorkoutExerciseViewModel<DurationAndWeightSetViewModel>
    {
        public DurationAndWeightWorkoutExerciseViewModel(ILoggingService logger,
            IMapper mapper,
            IModsSelectorModal modsSelectorModal,
            IWorkoutExercisesApiService workoutExercisesApi,
            IWorkoutExerciseModifiersApiService modsApi,
            IWorkoutExerciseSetsApiService setsApi,
            TotalEffortViewModel totalEffort)
            : base(logger, mapper, modsSelectorModal, workoutExercisesApi, modsApi, setsApi, totalEffort)
        {
        }

        override protected DurationAndWeightSetViewModel CreateSetVm(WorkoutExerciseSet set, int totalSets)
        {
            return new DurationAndWeightSetViewModel(set, totalSets);
        }

    }
}
