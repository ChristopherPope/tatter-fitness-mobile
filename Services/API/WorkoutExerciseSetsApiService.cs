using Flurl.Http.Configuration;
using TatterFitness.Models.Workouts;
using TatterFitness.App.Enums;
using TatterFitness.App.Interfaces.Services;
using TatterFitness.App.Interfaces.Services.API;
using TatterFitness.App.Models;

namespace TatterFitness.App.Services.API
{
    public class WorkoutExerciseSetsApiService : ApiServiceBase, IWorkoutExerciseSetsApiService
    {
        public WorkoutExerciseSetsApiService(ILoggingService logger, IFlurlClientFactory flurlClientFactory)
            : base("WorkoutExerciseSets", logger, flurlClientFactory)
        {
        }

        public async Task<WorkoutExerciseSet> Create(WorkoutExerciseSet set)
        {
            return await PerformPost<WorkoutExerciseSet>(CreateRequest(), set);
        }

        public async Task Delete(int workoutExerciseSetId)
        {
            await PerformDelete(CreateRequest(workoutExerciseSetId));
        }

        public async Task<WorkoutExerciseSet> Update(WorkoutExerciseSet set)
        {
            var patchOps = new List<PatchOperation>();

            patchOps.Add(new PatchOperation(PatchOpCommand.Replace, nameof(set.MilesDistance), set.MilesDistance));
            patchOps.Add(new PatchOperation(PatchOpCommand.Replace, nameof(set.MachineIntensity), set.MachineIntensity));
            patchOps.Add(new PatchOperation(PatchOpCommand.Replace, nameof(set.MachineWatts), set.MachineWatts));
            patchOps.Add(new PatchOperation(PatchOpCommand.Replace, nameof(set.MachineIncline), set.MachineIncline));
            patchOps.Add(new PatchOperation(PatchOpCommand.Replace, nameof(set.Calories), set.Calories));
            patchOps.Add(new PatchOperation(PatchOpCommand.Replace, nameof(set.MaxBpm), set.MaxBpm));
            patchOps.Add(new PatchOperation(PatchOpCommand.Replace, nameof(set.DurationInSeconds), set.DurationInSeconds));
            patchOps.Add(new PatchOperation(PatchOpCommand.Replace, nameof(set.Weight), set.Weight));
            patchOps.Add(new PatchOperation(PatchOpCommand.Replace, nameof(set.RepCount), set.RepCount));
            patchOps.Add(new PatchOperation(PatchOpCommand.Replace, nameof(set.Volume), set.Volume));
            patchOps.Add(new PatchOperation(PatchOpCommand.Replace, nameof(set.SetNumber), set.SetNumber));

            return await PerformPatch2<WorkoutExerciseSet>(CreateRequest(set.Id), patchOps);
        }
    }
}
