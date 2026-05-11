namespace Prokopovich_453503.Application.PirateUseCases.Commands
{
    public record MovePirateToCrewCommand(int PirateId, int NewCrewId) : IRequest { }

    internal class MovePirateToCrewCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<MovePirateToCrewCommand>
    {
        public async Task Handle(MovePirateToCrewCommand request, CancellationToken cancellationToken)
        {
            var pirate = await unitOfWork.PirateRepository
                .GetByIdAsync(request.PirateId, cancellationToken);
            if (pirate is null) return;

            pirate.MoveToCrew(request.NewCrewId);
            await unitOfWork.PirateRepository.UpdateAsync(pirate, cancellationToken);
            await unitOfWork.SaveAllAsync();
        }
    }
}