namespace Prokopovich_453503.Application.PirateUseCases.Commands
{
    public record DeletePirateCommand(int Id) : IRequest { }

    internal class DeletePirateCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<DeletePirateCommand>
    {
        public async Task Handle(DeletePirateCommand request, CancellationToken cancellationToken)
        {
            var pirate = await unitOfWork.PirateRepository
                .GetByIdAsync(request.Id, cancellationToken);
            if (pirate is null) return;

            await unitOfWork.PirateRepository.DeleteAsync(pirate, cancellationToken);
            await unitOfWork.SaveAllAsync();
        }
    }
}