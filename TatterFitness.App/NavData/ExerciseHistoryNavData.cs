namespace TatterFitness.App.NavData
{
    internal class ExerciseHistoryNavData : NavDataBase
    {
        public int ExerciseId { get; private set; }

        public ExerciseHistoryNavData(int exerciseId)
        {
            ExerciseId = exerciseId;
        }
    }
}
