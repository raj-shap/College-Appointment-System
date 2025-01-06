using College_Appointment_System.Interfaces;
using College_Appointment_System.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace College_Appointment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("Login")]
        public string Login([FromBody] LoginRequest loginRequest)
        {
            var token = _authService.Login(loginRequest);
            return token;
        }

        [HttpPost("AddUser")]
        public User AddUser([FromBody] User user)
        {
            var addUser = _authService.AddUser(user);
            return addUser;
        }

        [HttpPost("AddRole")]
        public Role AddRole([FromBody] Role role)
        {
            var addRole = _authService.AddRole(role);
            return addRole;
        }

        [HttpPost("AssignRole")]
        public bool AssignRoleToUser([FromBody] AddUserRole userRole)
        {
            var addedUserRole = _authService.AssignRoleToUser(userRole);
            return addedUserRole;
        }

    }
}
