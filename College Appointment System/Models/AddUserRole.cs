namespace College_Appointment_System.Models
{
    public class AddUserRole
    {
        public Guid UserId { get; set; }
        public List<Guid> RoleId { get; set; }
    }
}
