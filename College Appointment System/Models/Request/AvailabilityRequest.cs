namespace College_Appointment_System.Models.Request
{
    public class AvailabilityRequest
    {
        public Guid ProfessorId { get; set; }
        public List<DateTime> StartDate { get; set; }
        public List<DateTime> EndDate { get; set; }
    }
}
