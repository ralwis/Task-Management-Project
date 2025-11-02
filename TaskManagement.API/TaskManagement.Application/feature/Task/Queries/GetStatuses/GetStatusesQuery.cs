using MediatR;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.feature.Task.Queries.GetStatuses
{
    public record GetStatusesQuery : IRequest<List<StatusDTO>>;
}
