using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Application.DTOs
{
    [Table("Status")]
    public class StatusDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string DisplayName { get; init; }
    }
}
