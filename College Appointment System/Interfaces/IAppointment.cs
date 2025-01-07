using College_Appointment_System.Models;

namespace College_Appointment_System.Interfaces
{
    public interface IAppointment
    {
        Task<IEnumerable<Availability>> GetAvailabilities();
        Task<IEnumerable<Appointment>> GetAppointments();
        Task<Appointment> AddAppointment(Appointment appointment);
    }
}
