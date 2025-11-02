using MediatR;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.feature.Task.Queries.GetTaskById
{
    public record GetTaskByIdQuery(int Id) : IRequest<TaskDTO>;
}
