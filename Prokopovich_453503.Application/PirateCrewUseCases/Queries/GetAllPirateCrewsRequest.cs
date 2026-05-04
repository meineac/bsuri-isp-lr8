using System;
using System.Collections.Generic;
using System.Text;

namespace Prokopovich_453503.Application.PirateCrewUseCases.Queries
{
    public record GetAllPirateCrewsRequest() : IRequest<IEnumerable<PirateCrew>> { }

    internal class GetAllPirateCrewsRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllPirateCrewsRequest, IEnumerable<PirateCrew>>
    {
        public async Task<IEnumerable<PirateCrew>> Handle(
                GetAllPirateCrewsRequest request,
                CancellationToken cancellationToken)
        {
            return await unitOfWork.PiratCrewRepository.ListAllAsync(cancellationToken);
        }
    }
}   
