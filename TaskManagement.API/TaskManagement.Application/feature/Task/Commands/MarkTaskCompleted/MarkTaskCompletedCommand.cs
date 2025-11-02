using MediatR;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.feature.Task.Commands.MarkTaskCompleted
{
    public record MarkTaskCompletedCommand(int Id) : IRequest<UpdateResponseDTO>;
}
