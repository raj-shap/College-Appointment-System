using College_Appointment_System.Interfaces;
using College_Appointment_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace College_Appointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IAppointmentService _appointmentService;
        public StudentController(IStudentService studentService, IAppointmentService appointmentService)
        {
            _studentService = studentService;
            _appointmentService = appointmentService;
        }

        [HttpGet("GetStudents")]
        [Authorize(Roles = "Professor,Admin")]
        public List<User> GetStudents()
        {
            return _studentService.GetAllStudents();
        }
        
        //[HttpPost("AddStudent")]
        //[Authorize(Roles ="Professor,Admin")]
        //public Student AddStudent([FromBody]Student student)
        //{
        //    var addedStudent = _studentService.AddStudent(student);
        //    return addedStudent;
        //}

        [HttpPost("BookAppointment")]
        [Authorize(Roles ="Student")]
        public async Task<IActionResult> BookAppointment([FromBody] Appointment appointment)
        {
            var bookAppointment = await _appointmentService.AddAppointment(appointment);
            return Ok(bookAppointment);
        }

        //[HttpGet("{professorId}/AvailableAppointment")]
        //[Authorize(Roles = "Student,Admin")]
        //public async Task<IActionResult> GetAvailableAppointment(Guid professorId)
        //{
        //    var GetAvailable = await _appointmentService.GetNotBookedAvailabilities(professorId);
        //    return Ok(GetAvailable);
        //}

        [HttpGet("{ProfessorId}/AvailableAppointment")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> GetAvailableAppointment(Guid ProfessorId)
        {
            var availabilities = await _appointmentService.GetNotBookedAvailabilities(ProfessorId);
            Console.WriteLine($"\n\n\n\n show available : \n {availabilities} \n\n\n\n");
            return Ok(availabilities);
        }

        [HttpGet("{StudentId}/BookedAppointment")]
        [Authorize(Roles = "Student,Admin")]
        public async Task<IActionResult> GetBookedAppointment(Guid StudentId)
        {
            var BookedAppointment = await _appointmentService.GetAppointments(StudentId);
            return Ok(BookedAppointment);
        }

    }
}
