using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Domain.Entities
{
    [Table("Status")]
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
