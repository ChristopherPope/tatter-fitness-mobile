using AutoMapper;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;
using TatterFitness.Mobile.Interfaces.Services.SelectorModals;
using TatterFitness.Mobile.ViewModels;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.ViewModels.Workouts.WorkoutExercises
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

        override protected DurationAndWeightSetViewModel CreateSetVm(int exerciseId, WorkoutExerciseSet set, int totalSets)
        {
            return new DurationAndWeightSetViewModel(exerciseId, set, totalSets);
        }

    }
}
