using MediatR;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.feature.Task.Queries.GetAllTasks
{
    public class GetAllTasksHandler : IRequestHandler<GetAllTasksQuery, List<TaskDTO>>
    {
        private readonly ITaskService _taskService;

        public GetAllTasksHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<List<TaskDTO>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            return await _taskService.GetAllTasks(cancellationToken);
        }

    }
}
