using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Contracts;
using TaskManagement.Application.Contracts.Base;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Infrastructure.Services.Implementations
{
    public class TaskService : IBaseService, ITaskService
    {
        private readonly TaskContext _taskContext;
        private readonly IMapper _mapper;

        public TaskService(TaskContext taskContext, IMapper mapper)
        {
            _taskContext = taskContext;
            _mapper = mapper;
        }

        public async Task<List<TaskDTO>> GetAllTasks(CancellationToken cancellationToken)
        {
            return await (from t in _taskContext.Tasks
                          where !t.IsDeleted
                          select new TaskDTO
                          {
                              Id = t.Id,
                              Title = t.Title,
                              Description = t.Description,
                              CreatedDate = t.CreatedDate,
                              DueDate = t.DueDate,
                              CompletedDate = t.CompletedDate,
                              StatusId = t.StatusId
                          }).ToListAsync(cancellationToken);
        }

        public async Task<UpdateResponseDTO> CreateTask(TaskDTO task, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Domain.Entities.Task>(task);

            entity.CreatedDate = DateTime.UtcNow;

            _taskContext.Tasks.Add(entity);
            await _taskContext.SaveChangesAsync(cancellationToken);

            var status = await _taskContext.Statuses.FirstAsync(s => s.Id == entity.StatusId, cancellationToken);

            return new UpdateResponseDTO(entity.Id);
        }

        public async Task<UpdateResponseDTO> DeleteTask(int id, CancellationToken cancellationToken)
        {
            var entity = await _taskContext.Tasks.FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted, cancellationToken);
            if (entity is null)
            {
                throw new Exception($"Task with Id {id} was not found.");
            }

            entity.IsDeleted = true;
            await _taskContext.SaveChangesAsync(cancellationToken);
            return new UpdateResponseDTO(entity.Id);
        }

        public async Task<TaskDTO> GetTaskById(int id, CancellationToken cancellationToken)
        {
            var task = await(from t in _taskContext.Tasks
                         where !t.IsDeleted
                         select new TaskDTO
                         {
                             Id = t.Id,
                             Title = t.Title,
                             Description = t.Description,
                             CreatedDate = t.CreatedDate,
                             DueDate = t.DueDate,
                             CompletedDate = t.CompletedDate,
                             StatusId = t.StatusId
                         }).FirstOrDefaultAsync(cancellationToken);

            if(task is null)
            {
                throw new Exception("Invalid Task");
            }

            return task;
        }

        public async Task<UpdateResponseDTO> MarkTaskCompleted(int id, CancellationToken cancellationToken)
        {
            var completedStatus = await _taskContext.Statuses.FirstOrDefaultAsync(s => s.Name == "Completed", cancellationToken);
            if (completedStatus is null)
            {
                throw new Exception($"Invalid Status");
            }

            var entity = await _taskContext.Tasks.FirstOrDefaultAsync(t => t.Id == id && !t.IsDeleted, cancellationToken);
            if (entity is null)
            {
                throw new Exception($"Task with Id {id} was not found.");
            }

            entity.StatusId = completedStatus.Id;
            entity.CompletedDate = DateTime.UtcNow;

            await _taskContext.SaveChangesAsync(cancellationToken);
            return new UpdateResponseDTO(entity.Id);
        }

        public async Task<UpdateResponseDTO> UpdateTask(TaskDTO task, CancellationToken cancellationToken)
        {
            var entity = await _taskContext.Tasks.FirstOrDefaultAsync(t => t.Id == task.Id && !t.IsDeleted, cancellationToken);
            if (entity is null)
            {
                throw new Exception($"Task with Id {task.Id} was not found.");
            }

            _mapper.Map(task, entity);

            await _taskContext.SaveChangesAsync(cancellationToken);
            return new UpdateResponseDTO(entity.Id);
        }
    }
}
