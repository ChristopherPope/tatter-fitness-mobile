using CommunityToolkit.Maui;
using Syncfusion.Maui.Core.Hosting;
using TatterFitness.Mobile.Extensions;

namespace TatterFitness.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .RegisterServices()
                .RegisterViews()
                .RegisterViewModels()
                .RegisterEssentials()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-brands-400.ttf", "FAB");
                    fonts.AddFont("fa-regular-400.ttf", "FAR");
                    fonts.AddFont("fa-solid-900.ttf", "FAS");
                });

            return builder.Build();
        }
    }
}