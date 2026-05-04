using Prokopovich_453503.Persistance.Data;

namespace Prokopovich_453503.Persistance.Repository
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Lazy<IRepository<PirateCrew>> _pirateCrewRepository;
        private readonly Lazy<IRepository<Pirate>> _pirateRepository;
        public EfUnitOfWork(AppDbContext context)
        {
            _context = context;
            _pirateCrewRepository = new Lazy<IRepository<PirateCrew>>(() => new EfRepository<PirateCrew>(context));
            _pirateRepository = new Lazy<IRepository<Pirate>>(() => new EfRepository<Pirate>(context));
        }
        public IRepository<PirateCrew> PiratCrewRepository => _pirateCrewRepository.Value;
        public IRepository<Pirate> PirateRepository => _pirateRepository.Value;
        public async Task SaveAllAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task DeleteDataBaseAsync()
        {
            await _context.Database.EnsureDeletedAsync();
        }
        public async Task CreateDataBaseAsync()
        {
            await _context.Database.EnsureCreatedAsync();
        }
    }
}
