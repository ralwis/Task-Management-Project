using MediatR;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.feature.Task.Commands.UpdateTask
{
    public record UpdateTaskCommand(TaskDTO taskDTO) : IRequest<UpdateResponseDTO>;
}
