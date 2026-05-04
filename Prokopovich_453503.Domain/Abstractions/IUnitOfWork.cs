using Prokopovich_453503.Domain.Entities;

namespace Prokopovich_453503.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<PirateCrew> PiratCrewRepository { get; }
        IRepository<Pirate> PirateRepository { get; }
        Task SaveAllAsync();
        Task DeleteDataBaseAsync();
        Task CreateDataBaseAsync();
    }
}
