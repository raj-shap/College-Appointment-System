using System.ComponentModel.DataAnnotations;

namespace College_Appointment_System.Models
{
    public class UserRole
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
