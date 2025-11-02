using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Contracts
{
    public interface IStatusService
    {
        Task<List<StatusDTO>> GetStatuses(CancellationToken cancellationToken);
    }
}
