namespace Prokopovich_453503.Domain.Entities
{
    public enum CrewStatus
    {
        Active,
        OnHiatus,
        Allied,
        Subordinate,
        Disbanded,
        Annihilated,
        Imprisoned,
        Unknown
    }
    public class PirateCrew : Entity
    {
        private List<Pirate> _pirates = [];
        public string Name { get; private set; }
        public string Ship { get; set; }
        public CrewStatus Status { get; set; }
        public IReadOnlyList<Pirate> Pirates { get => _pirates.AsReadOnly(); }
        private PirateCrew() { }
        public PirateCrew(string name, string ship, CrewStatus status)
        {
            Name = name;
            Ship = ship;
            Status = status;
        }

    }
}
