using College_Appointment_System.Models;
using Microsoft.EntityFrameworkCore;

namespace College_Appointment_System.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Availability> Availability { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
