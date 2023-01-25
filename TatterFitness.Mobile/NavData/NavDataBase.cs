namespace TatterFitness.App.NavData
{
    public abstract class NavDataBase
    {
        private const string NavDataKey = "NavDataKey";

        public IDictionary<string, object> ToNavDataDictionary()
        {
            var navData = new Dictionary<string, object>
            {
                [NavDataKey] = this
            };

            return navData;
        }

        public static T FromNavDataDictionary<T>(IDictionary<string, object> dataDict) where T : NavDataBase
        {
            return dataDict[NavDataKey] as T;
        }
    }
}
