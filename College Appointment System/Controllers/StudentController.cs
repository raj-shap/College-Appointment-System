using College_Appointment_System.Interfaces;
using College_Appointment_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace College_Appointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("GetStudents")]
        public List<Student> GetStudents()
        {
            return _studentService.GetAllStudents();
        }
        [Authorize(Roles =("User"))]
        [HttpPost("AddStudent")]
        public Student AddStudent([FromBody]Student student)
        {
            var addedStudent = _studentService.AddStudent(student);
            return addedStudent;
        }

    }
}
