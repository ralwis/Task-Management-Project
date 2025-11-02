using MediatR;
using TaskManagement.Application.Contracts;

namespace TaskManagement.Application.feature.Task.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, bool>
    {
        private readonly ILoginService _loginService;

        public LoginHandler(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public async Task<bool> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _loginService.UserLogin(request.email, request.password, cancellationToken);

            return user != null;
        }
    }
}
