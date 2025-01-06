using System.ComponentModel.DataAnnotations;

namespace College_Appointment_System.Models
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
