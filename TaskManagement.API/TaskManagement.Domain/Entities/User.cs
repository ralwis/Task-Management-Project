using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Domain.Entities
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
