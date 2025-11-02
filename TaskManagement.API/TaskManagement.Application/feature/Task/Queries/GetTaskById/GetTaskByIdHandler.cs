using MediatR;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.feature.Task.Queries.GetTaskById
{
    public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, TaskDTO>
    {
        private readonly ITaskService _taskService;
        public GetTaskByIdHandler(ITaskService taskService) => _taskService = taskService;

        public async Task<TaskDTO?> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            return await _taskService.GetTaskById(request.Id, cancellationToken);
        }
    }
}
