using MediatR;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.feature.Task.Commands.UpdateTask
{
    public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, UpdateResponseDTO>
    {
        private readonly ITaskService _taskService;
        public UpdateTaskHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<UpdateResponseDTO> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            return await _taskService.UpdateTask(request.taskDTO, cancellationToken);
        }
    }
}
