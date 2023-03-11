using TatterFitness.Models.Workouts;

namespace TatterFitness.Mobile.Utils
{
    public class FTONotes
    {
        private readonly string delimiter = $"{Environment.NewLine}~FTO~{Environment.NewLine}";
        private readonly WorkoutExercise workoutExercise;

        public FTONotes(WorkoutExercise workoutExercise)
        {
            this.workoutExercise = workoutExercise;
            this.workoutExercise.Notes ??= string.Empty;
        }

        public void AddFTONotes(int trainingMax, int weekNumber)
        {
            Remove531Notes();
            workoutExercise.Notes = $"{workoutExercise.Notes}{delimiter}531 - Training Max: {trainingMax}, Week: {weekNumber}{delimiter}";
        }

        public string Get531Notes()
        {
            var (startIdx, length) = Find531NotesLocation();
            if (startIdx == -1)
            {
                return string.Empty;
            }

            return workoutExercise.Notes.Substring(startIdx, length);
        }

        public void Remove531Notes()
        {
            var (startIdx, length) = Find531NotesLocation();
            if (startIdx >= 0)
            {
                workoutExercise.Notes = workoutExercise.Notes.Remove(startIdx, length);
            }
        }

        private (int startIdx, int length) Find531NotesLocation()
        {
            var location = (startIdx: 0, length: 0);

            location.startIdx = workoutExercise.Notes.IndexOf(delimiter);
            if (location.startIdx >= 0)
            {
                location.length = workoutExercise.Notes.IndexOf(delimiter, location.startIdx + delimiter.Length) + delimiter.Length - location.startIdx;
            }

            return location;
        }
    }
}
