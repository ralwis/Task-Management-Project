using MediatR;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.feature.Task.Commands.CreateTask
{
    public record CreateTaskCommand(TaskDTO taskDTO) : IRequest<UpdateResponseDTO>;
}
