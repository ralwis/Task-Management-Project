using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using Task = TaskManagement.Domain.Entities.Task;

namespace TaskManagement.Infrastructure.Services
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
