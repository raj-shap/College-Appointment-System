namespace College_Appointment_System.Models
{
    public class Availability
    {
        public Guid Id { get; set; }
        public Guid ProfessorId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
