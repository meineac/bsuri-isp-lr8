namespace Prokopovich_453503.Persistance.Repository
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        private readonly Lazy<IRepository<PirateCrew>> _pirateCrewRepository;
        private readonly Lazy<IRepository<Pirate>> _pirateRepository;
        public FakeUnitOfWork()
        {
            _pirateCrewRepository = new Lazy<IRepository<PirateCrew>>(() => new FakePirateCrewRepository());
            _pirateRepository = new Lazy<IRepository<Pirate>>(() => new FakePirateRepository());
        }
        public IRepository<PirateCrew> PiratCrewRepository => _pirateCrewRepository.Value;
        public IRepository<Pirate> PirateRepository => _pirateRepository.Value;
        public async Task SaveAllAsync()
        {
            await Task.CompletedTask;   
        }
        public async Task CreateDataBaseAsync() => await Task.CompletedTask;
        public async Task DeleteDataBaseAsync() => await Task.CompletedTask;
    }
}
