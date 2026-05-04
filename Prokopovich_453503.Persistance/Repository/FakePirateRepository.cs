using System.Linq.Expressions;

namespace Prokopovich_453503.Persistance.Repository
{
    public class FakePirateRepository : IRepository<Pirate>
    {
        List<Pirate> _pirates;
        public FakePirateRepository()
        {
            _pirates = new List<Pirate>();

            var pirate = new Pirate("Monkey D. Luffy", 3_000_000_000, "Captain", ["Gomu Gomu No Mi", "Conquerors Haki"]);
            pirate.Id = 1;
            _pirates.Add(pirate);
            pirate.JoinCrew(1);

            pirate = new Pirate("Marshall D. Teach", 5_000_000_000, "Captain", ["Yami Yami No Mi", "Conquerors Haki"]);
            pirate.Id = 2;
            _pirates.Add(pirate);
            pirate.JoinCrew(2);

            pirate = new Pirate("Tony Tony Chopper", 200, "Doctor", ["Hito Hito No Mi", "Rumble ball"]);
            pirate.Id = 3;
            _pirates.Add(pirate);
            pirate.JoinCrew(1);

        }
        public async Task<Pirate?> GetByIdAsync(
                int id,
                CancellationToken cancellationToken = default,
                params Expression<Func<Pirate, object>>[]? includesProperties)
        {
            throw new NotImplementedException();
        }
        public async Task<IReadOnlyList<Pirate>> ListAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public async Task<IReadOnlyList<Pirate>> ListAsync(
                Expression<Func<Pirate, bool>> filter,
                CancellationToken cancellationToken = default,
                params Expression<Func<Pirate, object>>[]? includesProperties)
        {
            var compiledFilter = filter.Compile();
            return await Task.Run(() => _pirates.Where(compiledFilter).ToList() as IReadOnlyList<Pirate>, cancellationToken);
        }
        public Task AddAsync(Pirate entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public Task UpdateAsync(Pirate entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public Task DeleteAsync(Pirate entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public async Task<Pirate?> FirstOrDefaultAsync(
            Expression<Func<Pirate, bool>> filter,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

    }
}
