namespace TatterFitness.App.NavData.Select
{
    public class SelectModsNavData : NavDataBase
    {
        public IEnumerable<int> currentModIds { get; private set; }

        public SelectModsNavData(IEnumerable<int> currentModIds)
        {
            this.currentModIds = currentModIds;
        }
    }
}
