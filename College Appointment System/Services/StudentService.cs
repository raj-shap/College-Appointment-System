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
            if (student == null)
            {
                throw new Exception("Details is NULL");
            }
            try
            {
                student.Id = Guid.NewGuid();

                var studentUser = new User
                {
                    Id = student.Id,
                    Name = student.Name,
                    UserName = student.UserName,
                    Email = student.Email,
                    Role = student.Role,
                    Password = student.Password,
                };
                var userRole = new UserRole
                {
                    Id = Guid.NewGuid(),
                    UserId = student.Id,
                    RoleId = student.Role,
                };
                _context.Users.Add(studentUser);
                _context.UserRoles.Add(userRole);
                var std = _context.Students.Add(student);
                _context.SaveChanges();
                return std.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} : unexpected error occures while assign role to user.");
            }
        }

        public List<Student> GetAllStudents()
        {
            var student = _context.Students.ToList();
            if (student == null)
            {
                throw new Exception("Students Not Found");
            }
            try
            {
                return student;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} : unexpected error occures while assign role to user.");
            }
        }
    }
}
