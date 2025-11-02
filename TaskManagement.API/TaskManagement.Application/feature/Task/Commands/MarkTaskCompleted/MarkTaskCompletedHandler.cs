using MediatR;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.feature.Task.Commands.MarkTaskCompleted
{
    public class MarkTaskCompletedHandler : IRequestHandler<MarkTaskCompletedCommand, UpdateResponseDTO>
    {
        private readonly ITaskService _taskService;
        public MarkTaskCompletedHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<UpdateResponseDTO> Handle(MarkTaskCompletedCommand request, CancellationToken cancellationToken)
        {
            return await _taskService.MarkTaskCompleted(request.Id, cancellationToken);
        }
    }
}
