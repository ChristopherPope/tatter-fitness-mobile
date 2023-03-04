using AutoMapper;
using TatterFitness.Mobile.Interfaces.Services;
using TatterFitness.Mobile.Interfaces.Services.API;
using TatterFitness.Mobile.Interfaces.Services.SelectorModals;
using TatterFitness.Mobile.ViewModels;
using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.ViewModels.Workouts.WorkoutExercises
{
    public partial class RepsOnlyWorkoutExerciseViewModel : BaseWorkoutExerciseViewModel<RepsOnlySetViewModel>
    {
        public RepsOnlyWorkoutExerciseViewModel(ILoggingService logger,
            IMapper mapper,
            IModsSelectorModal modsSelectorModal,
            IWorkoutExercisesApiService workoutExercisesApi,
            IWorkoutExerciseModifiersApiService modsApi,
            IWorkoutExerciseSetsApiService setsApi,
            TotalEffortViewModel totalEffort)
            : base(logger, mapper, modsSelectorModal, workoutExercisesApi, modsApi, setsApi, totalEffort)
        {
        }

        override protected RepsOnlySetViewModel CreateSetVm(int exerciseId, WorkoutExerciseSet set, int totalSets)
        {
            return new RepsOnlySetViewModel(exerciseId, set, totalSets);
        }
    }
}
