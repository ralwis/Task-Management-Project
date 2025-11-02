using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Domain.Entities
{
    [Table("Task")]
    public class Task
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        public int StatusId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
