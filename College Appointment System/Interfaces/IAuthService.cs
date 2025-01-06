using College_Appointment_System.Models;

namespace College_Appointment_System.Interfaces
{
    public interface IAuthService
    {
        User AddUser(User user);
        string Login(LoginRequest loginRequest);

        Role AddRole(Role role);
        bool AssignRoleToUser(AddUserRole addUserRole);
    }
}
