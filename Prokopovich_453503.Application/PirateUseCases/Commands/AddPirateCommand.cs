namespace Prokopovich_453503.Application.PirateUseCases.Commands
{
    public record AddPirateCommand(string Name, long Bounty, string Role,
        List<string> Abilities, int? CrewId) : IRequest<Pirate> { }

    internal class AddPirateCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<AddPirateCommand, Pirate>
    {
        public async Task<Pirate> Handle(AddPirateCommand request,
            CancellationToken cancellationToken)
        {
            var pirate = new Pirate(request.Name, request.Bounty,
                request.Role, request.Abilities);

            if (request.CrewId.HasValue)
                pirate.JoinCrew(request.CrewId.Value);

            await unitOfWork.PirateRepository.AddAsync(pirate, cancellationToken);
            await unitOfWork.SaveAllAsync();
            return pirate;
        }
    }
}