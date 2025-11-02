using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Contracts
{
    public interface ILoginService
    {
        Task<User> UserLogin(string email, string password, CancellationToken cancellationToken);
        Task<User> UserRegister(string email, string password, CancellationToken cancellationToken);
    }
}
