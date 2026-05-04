namespace Prokopovich_453503.Application.PirateUseCases.Queries
{
    public record GetPiratesByCrewRequest(int CrewId) : IRequest<IEnumerable<Pirate>> { }

    internal class GetPiratesByCrewRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetPiratesByCrewRequest, IEnumerable<Pirate>>
    {
        public async Task<IEnumerable<Pirate>> Handle(
                GetPiratesByCrewRequest request,
                CancellationToken cancellationToken)
        {
            return await unitOfWork.PirateRepository.ListAsync(
                    p => p.CrewId == request.CrewId,
                    cancellationToken);
        }
    }
}
