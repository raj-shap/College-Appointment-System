using College_Appointment_System.Models;

namespace College_Appointment_System.Interfaces
{
    public interface IProfessorService
    {
        Task<IEnumerable<Professor>> GetProfessors();
        Task<Professor> AddProfessor (Professor professor);
        Task<Availability> AddAvailability(Availability available);
        

    }
}
