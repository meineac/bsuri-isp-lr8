using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Prokopovich_453503.Persistance.Data;
using Prokopovich_453503.Persistance.Repository;

namespace Prokopovich_453503.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWork, FakeUnitOfWork>();
            return services;
        }
        public static IServiceCollection AddPersistance(
                this IServiceCollection services,
                DbContextOptions options)
        {
            services.AddPersistance()
                    .AddSingleton<AppDbContext>(
                        new AppDbContext((DbContextOptions<AppDbContext>)options));
            return services;
        }
    }
}
