namespace Prokopovich_453503.Application.PirateCrewUseCases.Commands
{
    public record DeletePirateCrewCommand(int Id) : IRequest { }

    internal class DeletePirateCrewCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<DeletePirateCrewCommand>
    {
        public async Task Handle(DeletePirateCrewCommand request, CancellationToken cancellationToken)
        {
            var crew = await unitOfWork.PiratCrewRepository
                .GetByIdAsync(request.Id, cancellationToken);
            if (crew is null) return;

            await unitOfWork.PiratCrewRepository.DeleteAsync(crew, cancellationToken);
            await unitOfWork.SaveAllAsync();
        }
    }
}