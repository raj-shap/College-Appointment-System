using College_Appointment_System.Interfaces;
using College_Appointment_System.Models;
using College_Appointment_System.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace College_Appointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorService _professorService;
        private readonly IAppointmentService _appointmentService;
        public ProfessorController(IProfessorService professorService, IAppointmentService appointmentService)
        {
            _professorService = professorService;
            _appointmentService = appointmentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProfessors()
        {
            var professors = await _professorService.GetProfessors();
            return Ok(professors);
        }
        [HttpPost("AddProfessor")]
        public async Task<IActionResult> AddProfessor([FromBody] Professor professor)
        {
            var addProfessor = await _professorService.AddProfessor(professor);
            return Ok(addProfessor);
        }
        [HttpPost("AddAvailability")]
        public async Task<IActionResult> AddAvailability([FromBody] AvailabilityRequest availability)
        {
            var availabilityReq = await _professorService.AddAvailability(availability);
            return Ok(availabilityReq);
        }
        [HttpGet("{ProfessorId}/Booked")]
        public async Task<IActionResult> GetBookedAvailabilities([FromBody]Guid ProfessorId)
        {
            var availabilities =await _appointmentService.GetBookedAvailabilities(ProfessorId);
            return Ok(availabilities);
        }

        [HttpGet("{ProfessorId}/NotBooked")]
        public async Task<IActionResult> GetNotBookedAvailabilities([FromBody]Guid ProfessorId)
        {
            var availabilities = await _appointmentService.GetNotBookedAvailabilities(ProfessorId);
            return Ok(availabilities);
        }
    }
}
