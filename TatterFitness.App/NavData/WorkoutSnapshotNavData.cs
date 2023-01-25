using TatterFitness.Models;

namespace TatterFitness.App.NavData
{
    public class WorkoutSnapshotNavData : NavDataBase
    {
        public int WorkoutId { get; }

        public WorkoutSnapshotNavData(int workoutId)
        {
            WorkoutId = workoutId;
        }
    }
}
