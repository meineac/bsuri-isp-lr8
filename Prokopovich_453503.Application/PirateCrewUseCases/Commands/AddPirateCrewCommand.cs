namespace Prokopovich_453503.Application.PirateCrewUseCases.Commands
{
    public record AddPirateCrewCommand(string Name, string Ship, CrewStatus Status)
        : IRequest<PirateCrew> { }

    internal class AddPirateCrewCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<AddPirateCrewCommand, PirateCrew>
    {
        public async Task<PirateCrew> Handle(AddPirateCrewCommand request,
            CancellationToken cancellationToken)
        {
            var crew = new PirateCrew(request.Name, request.Ship, request.Status);
            await unitOfWork.PiratCrewRepository.AddAsync(crew, cancellationToken);
            await unitOfWork.SaveAllAsync();
            return crew;
        }
    }
}