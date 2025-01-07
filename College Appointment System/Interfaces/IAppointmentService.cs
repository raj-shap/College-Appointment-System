using College_Appointment_System.Models;

namespace College_Appointment_System.Interfaces
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Availability>> GetBookedAvailabilities(Guid ProfessorId);
        Task<IEnumerable<Availability>> GetNotBookedAvailabilities(Guid ProfessorId);
        Task<IEnumerable<Appointment>> GetAppointments(Guid StudentId);
        Task<Appointment> AddAppointment(Appointment appointment);
    }
}
