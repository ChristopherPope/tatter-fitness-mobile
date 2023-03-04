namespace TatterFitness.Mobile.NavData.Select
{
    public class SelectExercisesNavData : NavDataBase
    {
        public IEnumerable<int> RequiredExerciseIds { get; private set; } = Enumerable.Empty<int>();
        public IEnumerable<int> CurrentExerciseIds { get; private set; }
        public bool AllowOnlyOneSelection { get; private set; }

        public SelectExercisesNavData(IEnumerable<int> currentExerciseIds)
        {
            CurrentExerciseIds = currentExerciseIds;
        }

        public SelectExercisesNavData(IEnumerable<int> currentExerciseIds, IEnumerable<int> requiredExerciseIds)
        {
            CurrentExerciseIds = currentExerciseIds;
            RequiredExerciseIds = requiredExerciseIds;
        }

        public SelectExercisesNavData(int currentExerciseId)
        {
            CurrentExerciseIds = new List<int>() { currentExerciseId };
            AllowOnlyOneSelection = true;
        }
    }
}
