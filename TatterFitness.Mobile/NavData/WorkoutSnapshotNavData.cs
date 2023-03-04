using TatterFitness.Models;

namespace TatterFitness.Mobile.NavData
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
