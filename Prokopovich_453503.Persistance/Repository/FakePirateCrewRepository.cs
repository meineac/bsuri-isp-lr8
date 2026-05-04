using System.Linq.Expressions;

namespace Prokopovich_453503.Persistance.Repository
{
    public class FakePirateCrewRepository : IRepository<PirateCrew>
    {
        List<PirateCrew> _crews;
        public FakePirateCrewRepository()
        {
            _crews = new List<PirateCrew>();

            var crew = new PirateCrew("Straw Hats", "Going Merry", CrewStatus.Active);
            crew.Id = 1;
            _crews.Add(crew);

            crew = new PirateCrew("Black Beard Pirates", "Saber of Xebec", CrewStatus.Active);
            crew.Id = 2;
            _crews.Add(crew);
        }
        public async Task<PirateCrew?> GetByIdAsync(
                int id, 
                CancellationToken cancellationToken = default, 
                params Expression<Func<PirateCrew, object>>[]? includesProperties)
        {
            return await Task.Run(() => _crews.FirstOrDefault(c => c.Id == id), cancellationToken);
        }
        public async Task<IReadOnlyList<PirateCrew>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => _crews.AsReadOnly() as IReadOnlyList<PirateCrew>, cancellationToken);
        }
        public async Task<IReadOnlyList<PirateCrew>> ListAsync(
                Expression<Func<PirateCrew, bool>> filter, 
                CancellationToken cancellationToken = default, 
                params Expression<Func<PirateCrew, object>>[]? includesProperties)
        {
            var compiledFilter = filter.Compile();
            return await Task.Run(() => _crews.Where(compiledFilter).ToList() as IReadOnlyList<PirateCrew>, cancellationToken);
        }
        public Task AddAsync(PirateCrew entity, CancellationToken cancellationToken = default)
        {
            _crews.Add(entity);
            return Task.CompletedTask;
        }
        public Task UpdateAsync(PirateCrew entity, CancellationToken cancellationToken = default)
        {
            var existing = _crews.FirstOrDefault(c => c.Id == entity.Id);
            if (existing != null)
            {
                _crews.Remove(existing);
                _crews.Add(entity);
            }
            return Task.CompletedTask;
        }
        public Task DeleteAsync(PirateCrew entity, CancellationToken cancellationToken = default)
        {
            var existing = _crews.FirstOrDefault(c => c.Id == entity.Id);
            if (existing != null)
            {
                _crews.Remove(existing);
            }
            return Task.CompletedTask;
        }
        public async Task<PirateCrew?> FirstOrDefaultAsync(
            Expression<Func<PirateCrew, bool>> filter, 
            CancellationToken cancellationToken = default)
        {
            var compiledFilter = filter.Compile();
            return await Task.Run(() => _crews.FirstOrDefault(compiledFilter), cancellationToken);
        }
    }
}
