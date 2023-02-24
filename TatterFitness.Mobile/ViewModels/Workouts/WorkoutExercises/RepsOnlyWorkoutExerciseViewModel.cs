using AutoMapper;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Interfaces.Services.API;
using TatterFitness.App.Interfaces.Services.SelectorModals;
using TatterFitness.Mobile.ViewModels;
using TatterFitness.Models.Workouts;

namespace TatterFitness.App.ViewModels.Workouts.WorkoutExercises
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
