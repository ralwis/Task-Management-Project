using MediatR;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.feature.Task.Commands.DeleteTask
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, UpdateResponseDTO>
    {
        private readonly ITaskService _taskService;
        public DeleteTaskHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<UpdateResponseDTO> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            return await _taskService.DeleteTask(request.Id, cancellationToken);
        }
    }
}
