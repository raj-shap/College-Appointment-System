using College_Appointment_System.Context;
using College_Appointment_System.Interfaces;
using College_Appointment_System.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace College_Appointment_System.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        public StudentService (ApplicationDbContext context)
        {
            _context = context;
        }
        public Student AddStudent(Student student)
        {
            var std = _context.Students.Add(student);
            _context.SaveChanges();
            return std.Entity;
        }

        public List<Student> GetAllStudents()
        {
            var student = _context.Students.ToList();
            return student;
        }
    }
}
