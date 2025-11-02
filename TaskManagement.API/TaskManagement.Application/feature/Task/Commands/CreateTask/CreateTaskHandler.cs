using MediatR;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.feature.Task.Commands.CreateTask
{
    public class CreateTaskHandler : IRequestHandler<CreateTaskCommand, UpdateResponseDTO>
    {
        private readonly ITaskService _taskService;
        public CreateTaskHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<UpdateResponseDTO> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            return await _taskService.CreateTask(request.taskDTO, cancellationToken);
        }
    }
}
