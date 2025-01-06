using College_Appointment_System.Context;
using College_Appointment_System.Helpers;
using College_Appointment_System.Interfaces;
using College_Appointment_System.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace College_Appointment_System.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Role AddRole(Role role)
        {
            role.Id = Guid.NewGuid();
            var addedRole = _context.Roles.Add(role);
            if(addedRole == null)
            {
                throw new Exception("Details is null.");
            }
            try
            {
                _context.SaveChanges();
                return addedRole.Entity;
            }catch (Exception ex)
            {
                throw new Exception($"{ex.Message} : unexpected error occures while assign role to user.");
            }
        }

        public User AddUser(User user)
        {
            if(user == null)
            {
                throw new Exception("Details is null");
            }
            try
            {
                user.Id = Guid.NewGuid();
                var addedUser = _context.Users.Add(user);
                _context.SaveChanges();
                return addedUser.Entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} : unexpected error occures while assign role to user.");
            }
        }

        public bool AssignRoleToUser(AddUserRole addUserRole)
        {
            try
            {
                var addRole = new List<UserRole>();
                var user = _context.Users.SingleOrDefault(u=> u.Id == addUserRole.UserId);
                if (user == null)
                {
                    throw new Exception("User Not Found");
                }
                foreach(var roles in addUserRole.RoleId)
                {
                    var userRole = new UserRole();
                    userRole.RoleId = roles;
                    userRole.UserId = user.Id;
                    addRole.Add(userRole);
                }
                _context.UserRoles.AddRange(addRole);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} : unexpected error occures while assign role to user.");
            }
        }

        public string Login(LoginRequest loginRequest)
        {
            if (loginRequest.UserName != null && loginRequest.Password != null)
            {
                var user = _context.Users.SingleOrDefault(s => s.UserName == loginRequest.UserName && s.Password == loginRequest.Password);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,_configuration["Jwt:Subject"]),
                        new Claim("Id",user.Id.ToString()),
                        new Claim("UserName",user.UserName),
                        new Claim("Email",user.Email)
                    };
                    var userRoles = _context.UserRoles.Where(u => u.UserId == user.Id).ToList();
                    var roleId = userRoles.Select(s => s.RoleId).ToList();
                    var roles = _context.Roles.Where(r => roleId.Contains(r.Id)).ToList();
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim("Role", role.Name));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(1),
                        signingCredentials: signIn
                        );
                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;
                }
                else
                {
                    return "User Not Valid";
                    //throw new Exception("user is not valid");
                }
            }
            else
            {
                throw new Exception("Invalid Credentials");
            }
        }
    }
}
