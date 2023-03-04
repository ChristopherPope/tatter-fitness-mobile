using TatterFitness.Models;

namespace TatterFitness.Mobile.NavData
{
    public class WorkoutNavData : NavDataBase
    {
        public int RoutineId { get; }

        public WorkoutNavData(int routineId)
        {
            RoutineId = routineId;
        }
    }
}
