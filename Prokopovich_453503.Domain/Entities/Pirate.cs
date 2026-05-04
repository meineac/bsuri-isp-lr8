namespace Prokopovich_453503.Domain.Entities
{
    public class Pirate : Entity
    {
        public string Name { get; private set; }
        public long Bounty  { get; private set; }
        public string Role { get; private set; }
        public List<string> Abilities { get; private set; }
        public int? CrewId { get; private set; }
        public PirateCrew? Crew { get; private set; }

        private Pirate() { }
        public Pirate(string name, long bounty, string role, List<string>? abilities=null)
        {
            Name = name;
            Bounty = bounty;
            Role = role;
            Abilities = abilities ?? [];
        }
        public void JoinCrew(int crewId)
        {
            CrewId ??= crewId;
        }
        public void LeaveCrew()
        {
            CrewId = null;
        }
        public void UpdateBounty(long bounty)
        {
            if (bounty > 0)
            {
                Bounty = bounty;
            }
        }
    }
}
