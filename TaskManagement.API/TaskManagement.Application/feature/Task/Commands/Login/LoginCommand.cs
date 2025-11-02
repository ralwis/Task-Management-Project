using MediatR;

namespace TaskManagement.Application.feature.Task.Commands.Login
{
    public record LoginCommand(string email, string password) : IRequest<bool>;
}
