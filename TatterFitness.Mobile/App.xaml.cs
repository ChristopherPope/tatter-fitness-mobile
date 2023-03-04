using Syncfusion.Licensing;
using System.Reflection;

namespace TatterFitness.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            RegisterSyncFusionLicense();
            InitializeComponent();

            MainPage = new AppShell();
        }

        public static void RegisterSyncFusionLicense()
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var name = assembly.GetName().Name;
            if (!(assembly != null) || string.IsNullOrEmpty(name))
            {
                return;
            }

            string text = "SyncfusionLicense.txt";
            string licenseKey = string.Empty;
            using Stream stream = assembly!.GetManifestResourceStream(name + "." + text);
            if (stream != null)
            {
                using (var streamReader = new StreamReader(stream))
                {
                    licenseKey = streamReader.ReadToEnd();
                }

                SyncfusionLicenseProvider.RegisterLicense(licenseKey);
            }
        }
    }
}