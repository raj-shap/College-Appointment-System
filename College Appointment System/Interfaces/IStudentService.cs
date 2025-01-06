using College_Appointment_System.Models;

namespace College_Appointment_System.Interfaces
{
    public interface IStudentService
    {
        List<Student> GetAllStudents();
        Student AddStudent(Student student);
    }
}
