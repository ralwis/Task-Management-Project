using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.Contracts.Base;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Infrastructure.Services.Implementations
{
    public class StatusService : IBaseService, IStatusService
    {
        private readonly TaskContext _taskContext;

        public StatusService(TaskContext taskContext)
        {
            _taskContext = taskContext;
        }
        public async Task<List<StatusDTO>> GetStatuses(CancellationToken cancellationToken)
        { 
            return await(from s in _taskContext.Statuses
                         select new StatusDTO
                         {
                             Id = s.Id,
                             Name = s.Name,
                             DisplayName = s.DisplayName
                         }).ToListAsync(cancellationToken);
        }
    }
}
