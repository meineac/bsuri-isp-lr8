namespace Prokopovich_453503.Application.PirateCrewUseCases.Commands
{
    public record UpdatePirateCrewCommand(int Id, string Name, string Ship, CrewStatus Status)
        : IRequest { }

    internal class UpdatePirateCrewCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<UpdatePirateCrewCommand>
    {
        public async Task Handle(UpdatePirateCrewCommand request, CancellationToken cancellationToken)
        {
            var crew = await unitOfWork.PiratCrewRepository
                .GetByIdAsync(request.Id, cancellationToken);
            if (crew is null) return;

            crew.Update(request.Name, request.Ship, request.Status);
            await unitOfWork.PiratCrewRepository.UpdateAsync(crew, cancellationToken);
            await unitOfWork.SaveAllAsync();
        }
    }
}