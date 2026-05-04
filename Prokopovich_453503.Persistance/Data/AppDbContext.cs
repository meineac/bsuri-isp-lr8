using Microsoft.EntityFrameworkCore;

namespace Prokopovich_453503.Persistance.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<PirateCrew> PirateCrews => Set<PirateCrew>();
        public DbSet<Pirate> Pirates => Set<Pirate>();
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
