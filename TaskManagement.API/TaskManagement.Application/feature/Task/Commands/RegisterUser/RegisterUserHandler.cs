using MediatR;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.feature.Task.Commands.Login;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.feature.Task.Commands.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly ILoginService _loginService;

        public RegisterUserHandler(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<bool> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _loginService.UserRegister(request.email, request.password, cancellationToken);

            return user != null;
        }
    }
}
