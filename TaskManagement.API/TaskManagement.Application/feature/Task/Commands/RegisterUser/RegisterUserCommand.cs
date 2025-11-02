using MediatR;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.feature.Task.Commands.RegisterUser
{
    public record RegisterUserCommand(string email, string password) : IRequest<bool>;
}
