namespace Prokopovich_453503.Application.PirateUseCases.Commands
{
    public record UpdatePirateCommand(int Id, string Name, long Bounty,
        string Role, List<string> Abilities) : IRequest { }

    internal class UpdatePirateCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<UpdatePirateCommand>
    {
        public async Task Handle(UpdatePirateCommand request, CancellationToken cancellationToken)
        {
            var pirate = await unitOfWork.PirateRepository
                .GetByIdAsync(request.Id, cancellationToken);
            if (pirate is null) return;

            pirate.Update(request.Name, request.Bounty, request.Role, request.Abilities);
            await unitOfWork.PirateRepository.UpdateAsync(pirate, cancellationToken);
            await unitOfWork.SaveAllAsync();
        }
    }
}