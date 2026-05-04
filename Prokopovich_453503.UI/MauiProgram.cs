using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Prokopovich_453503.Application;
using Prokopovich_453503.Persistance;
using Prokopovich_453503.UI.Pages;
using Prokopovich_453503.UI.ViewModels;

namespace Prokopovich_453503.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services
                .AddApplication()
                .AddPersistance()
                .RegisterPages()
                .RegisterViewModels();

            builder.Services.AddTransient<PirateCrewsViewModel>();
            builder.Services.AddTransient<PirateCrews>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
