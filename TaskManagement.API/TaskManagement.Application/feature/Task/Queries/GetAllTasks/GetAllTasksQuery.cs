using MediatR;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.feature.Task.Queries.GetAllTasks
{
    public record GetAllTasksQuery : IRequest<List<TaskDTO>>;
}
