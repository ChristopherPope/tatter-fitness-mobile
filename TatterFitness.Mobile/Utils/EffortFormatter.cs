namespace TatterFitness.Mobile.Utils
{
    public class EffortFormatter
    {
        public string RWVolume { get; private set; }
        public string ROReps { get; private set; }
        public string DWVolume { get; private set; }
        public string DWDuration { get; private set; }
        public string CMiles { get; private set; }
        public string CDuration { get; private set; }
        public string CBpm { get; private set; }

        public void FormatEffort(EffortCalculator effortCalculator)
        {
            RWVolume = $"{effortCalculator.RWVolume:#,0}";
            ROReps = effortCalculator.ROReps.ToString();
            DWVolume = $"{effortCalculator.DWVolume:#,0}";
            DWDuration = TimeSpan.FromSeconds(effortCalculator.DWDurationInSeconds).ToString();
            CMiles = $"{effortCalculator.CMiles:#,0.00}";
            CDuration = TimeSpan.FromSeconds(effortCalculator.CDurationInSeconds).ToString();
            CBpm = effortCalculator.CBpm.ToString();

        }
    }
}
