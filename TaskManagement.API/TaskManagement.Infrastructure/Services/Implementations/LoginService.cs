using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.Contracts.Base;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Services.Implementations
{
    public class LoginService : IBaseService, ILoginService
    {
        private readonly TaskContext _taskContext;

        public LoginService(TaskContext taskContext)
        {
            _taskContext = taskContext;
        }
        public async Task<User> UserLogin(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _taskContext.Users
                        .FirstOrDefaultAsync(u => u.Email == email && u.Password == password, cancellationToken);

            return user;
        }

        public async Task<User> UserRegister(string email, string password, CancellationToken cancellationToken)
        {
            var existingUser = await _taskContext.Users
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

            if (existingUser is not null)
            {
                throw new Exception("Username already exists");
            }
                
            var user = new User
            {
                Email = email,
                Password = password
            };

            _taskContext.Users.Add(user);
            await _taskContext.SaveChangesAsync(cancellationToken);

            return user;
        }
    }
}
