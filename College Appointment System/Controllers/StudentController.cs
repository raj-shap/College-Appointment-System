﻿using College_Appointment_System.Interfaces;
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
        [Authorize(Roles = "Professor,Admin")]
        [HttpGet("GetStudents")]
        public List<Student> GetStudents()
        {
            return _studentService.GetAllStudents();
        }
        [Authorize(Roles ="Professor,Admin")]
        [HttpPost("AddStudent")]
        public Student AddStudent([FromBody]Student student)
        {
            var addedStudent = _studentService.AddStudent(student);
            return addedStudent;
        }

        [Authorize(Roles ="Student")]
        [HttpPost("BookAppointment")]
        public IActionResult BookAppointment([FromBody] Appointment appointment)
        {
            var bookAppointment = _appointmentService.AddAppointment(appointment);
            return Ok(bookAppointment);
        }
        [Authorize(Roles = "Student,Admin")]
        [HttpGet("{professorId}/AvailableAppointment")]
        public IActionResult GetAvailableAppointment(Guid professorId)
        {
            var GetEmployee = _appointmentService.GetNotBookedAvailabilities(professorId);
            return Ok(GetEmployee);
        }
        [Authorize(Roles = "Student,Admin")]
        [HttpGet("{StudentId}/BookedAppointment")]
        public IActionResult GetBookedAppointment(Guid StudentId)
        {
            var BookedAppointment = _appointmentService.GetAppointments(StudentId);
            return Ok(BookedAppointment);
        }
    }
}
