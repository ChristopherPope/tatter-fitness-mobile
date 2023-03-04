using TatterFitness.Models;

namespace TatterFitness.Mobile.NavData
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
