using MediatR;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.feature.Task.Queries.GetStatuses
{
    public class GetStatusesHandler : IRequestHandler<GetStatusesQuery, List<StatusDTO>>
    {
        private readonly IStatusService _statusService;

        public GetStatusesHandler(IStatusService statusService)
        {
            _statusService = statusService;
        }

        public async Task<List<StatusDTO>> Handle(GetStatusesQuery request, CancellationToken cancellationToken)
        {
            return await _statusService.GetStatuses(cancellationToken);
        }
    }
}
