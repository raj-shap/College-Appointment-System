using College_Appointment_System.Models;

namespace College_Appointment_System.Interfaces
{
    public interface IStudentService
    {
        List<User> GetAllStudents();
        //Student AddStudent(Student student);
    }
}
