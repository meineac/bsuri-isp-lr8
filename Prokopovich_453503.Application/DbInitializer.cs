using Microsoft.Extensions.DependencyInjection;

namespace Prokopovich_453503.Application
{
    public static class DbInitializer
    {
        public static async Task Initialize(IServiceProvider services)
        {
            var unitOfWork = services.GetRequiredService<IUnitOfWork>();

            await unitOfWork.CreateDataBaseAsync();

            var pir = await unitOfWork.PirateRepository.ListAllAsync();

            if (pir.Any())
                return;

            var strawHats = new PirateCrew("Straw Hats", "Thousand Sunny", CrewStatus.Active);
            var blackBeards = new PirateCrew("Black Beard Pirates", "Saber of Xebec", CrewStatus.Active);
            var beastsPirates = new PirateCrew("Beasts Pirates", "Mammoth", CrewStatus.Annihilated);

            await unitOfWork.PiratCrewRepository.AddAsync(strawHats);
            await unitOfWork.PiratCrewRepository.AddAsync(blackBeards);
            await unitOfWork.PiratCrewRepository.AddAsync(beastsPirates);

            await unitOfWork.SaveAllAsync();

            // Добавить пиратов — не забудьте записать в команду через JoinCrew
            var pirates = new List<Pirate>
            {
                new Pirate("Monkey D. Luffy", 3_000_000_000, "Captain", ["Gomu Gomu no Mi", "Conquerors Haki"]),
                new Pirate("Roronoa Zoro", 1_111_000_000, "Swordsman"),
                new Pirate("Nami", 366_000_000, "Navigator"),
                new Pirate("Usopp", 500_000_000, "Sniper"),
                new Pirate("Sanji", 1_032_000_000, "Cook"),
                new Pirate("Marshall D. Teach", 3_996_000_000, "Captain"),
                new Pirate("Jesus Burgess", 100_000_000, "Helmsman"),
                new Pirate("Shiryu", 950_000_000, "Swordsman"),
                new Pirate("Kaido", 4_611_100_000, "Captain"),
                new Pirate("King", 1_390_000_000, "Commander"),
                new Pirate("Queen", 1_320_000_000, "Commander"),
            };

            pirates[0].JoinCrew(strawHats.Id);
            pirates[1].JoinCrew(strawHats.Id);
            pirates[2].JoinCrew(strawHats.Id);
            pirates[3].JoinCrew(strawHats.Id);
            pirates[4].JoinCrew(strawHats.Id);

            pirates[5].JoinCrew(blackBeards.Id);
            pirates[6].JoinCrew(blackBeards.Id);
            pirates[7].JoinCrew(blackBeards.Id);

            pirates[8].JoinCrew(beastsPirates.Id);
            pirates[9].JoinCrew(beastsPirates.Id);
            pirates[10].JoinCrew(beastsPirates.Id);

            foreach (var pirate in pirates)
                await unitOfWork.PirateRepository.AddAsync(pirate);

            await unitOfWork.SaveAllAsync();
        }
    }
}