namespace College_Appointment_System.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public Guid ProfessorId { get; set; }
        public Guid AvailabilityId { get; set; }
    }
}
