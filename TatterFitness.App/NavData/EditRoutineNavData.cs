using TatterFitness.Models;

namespace TatterFitness.App.NavData
{
    public class EditRouitineNavData : NavDataBase
    {
        public Routine Routine { get; private set; }

        public EditRouitineNavData(Routine routine)
        {
            Routine = routine;
        }
    }
}
