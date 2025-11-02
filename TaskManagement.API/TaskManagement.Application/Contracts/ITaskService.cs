using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Contracts
{
    public interface ITaskService
    {
        Task<List<TaskDTO>> GetAllTasks(CancellationToken cancellationToken);
        Task<TaskDTO> GetTaskById(int id, CancellationToken cancellationToken);
        Task<UpdateResponseDTO> CreateTask(TaskDTO task, CancellationToken cancellationToken);
        Task<UpdateResponseDTO> UpdateTask(TaskDTO task, CancellationToken cancellationToken);
        Task<UpdateResponseDTO> DeleteTask(int id, CancellationToken cancellationToken);
        Task<UpdateResponseDTO> MarkTaskCompleted(int id, CancellationToken cancellationToken);
    }
}
