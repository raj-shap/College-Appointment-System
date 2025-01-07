using College_Appointment_System.Models;
using College_Appointment_System.Models.Request;

namespace College_Appointment_System.Interfaces
{
    public interface IProfessorService
    {
        Task<IEnumerable<Professor>> GetProfessors();
        Task<Professor> AddProfessor (Professor professor);
        Task<bool> AddAvailability(AvailabilityRequest availableReq);
    }
}
