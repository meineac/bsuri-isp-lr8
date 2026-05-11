using Prokopovich_453503.UI.Pages;
using Prokopovich_453503.UI.ViewModels;

namespace Prokopovich_453503.UI
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterPages(this IServiceCollection services)
        {
            services.AddTransient<PirateCrews>()
                    .AddTransient<PirateDetails>()
                    .AddTransient<AddEditCrewPage>()
                    .AddTransient<AddEditPiratePage>();
            return services;
        }

        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddTransient<PirateCrewsViewModel>()
                    .AddTransient<PirateDetailsViewModel>()
                    .AddTransient<AddEditCrewViewModel>()
                    .AddTransient<AddEditPirateViewModel>();
            return services;
        }
    }
}
