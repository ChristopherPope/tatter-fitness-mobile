using CommunityToolkit.Maui;
using TatterFitness.App.Extensions;

namespace TatterFitness.App
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