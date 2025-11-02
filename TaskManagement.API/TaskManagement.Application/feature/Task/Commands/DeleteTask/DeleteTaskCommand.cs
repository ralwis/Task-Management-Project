using MediatR;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.feature.Task.Commands.DeleteTask
{
    public record DeleteTaskCommand(int Id) : IRequest<UpdateResponseDTO>;
}
