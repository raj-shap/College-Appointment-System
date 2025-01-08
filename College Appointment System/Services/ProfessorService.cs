using College_Appointment_System.Context;
using College_Appointment_System.Interfaces;
using College_Appointment_System.Models;
using College_Appointment_System.Models.Request;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace College_Appointment_System.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly ApplicationDbContext _context;
        public ProfessorService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAvailability(AvailabilityRequest available)
        {
            if(available == null)
            {
                throw new ArgumentNullException(nameof(available));
            }
            try
            {
                var profRole = _context.Roles.SingleOrDefault(r => r.Name == "Professor");
                var professor = _context.Users.SingleOrDefault(p => (p.Id == available.ProfessorId) && (p.Role == profRole.Id));
                
                if (professor != null)
                {
                    var addavailable = new List<Availability>();
                    for (int i=0; i<available.StartDate.Count; i++)
                    {
                        var profAvailability = new Availability();
                        profAvailability.Id = Guid.NewGuid();
                        profAvailability.ProfessorId = available.ProfessorId;
                        profAvailability.StartDate = available.StartDate[i];
                        profAvailability.EndDate = available.EndDate[i];
                        if (profAvailability.StartDate <= DateTime.UtcNow && profAvailability.EndDate <= DateTime.UtcNow)
                        {
                            throw new ArgumentException("Date cannot be in the past.");
                        }
                        if(profAvailability.StartDate == profAvailability.EndDate)
                        {
                            throw new ArgumentException("End Date or Time should not be same as start date or Time.");
                        }
                        bool hasOverlap = _context.Availability
                            .Where(p => p.ProfessorId == available.ProfessorId)
                            .Any(existing => 
                                (profAvailability.StartDate >= existing.StartDate && profAvailability.StartDate < existing.EndDate) ||
                                (profAvailability.EndDate > existing.EndDate && profAvailability.EndDate <= existing.EndDate) ||
                                (profAvailability.StartDate <= existing.StartDate && profAvailability.EndDate >= existing.EndDate) ||
                                (profAvailability.EndDate < existing.StartDate || profAvailability.StartDate > existing.EndDate)
                                //(profAvailability.EndDate < existing.StartDate || profAvailability.StartDate > existing.EndDate)
                            );
                        if (hasOverlap)
                        {
                            throw new Exception ($"The Availability Date Time is overlaps with an existing availability Date Time. Requested{profAvailability.StartDate.ToString()} to {profAvailability.EndDate.ToString()}");
                        }

                        addavailable.Add(profAvailability);
                    }
                    await _context.Availability.AddRangeAsync(addavailable);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new Exception($"Proffesor with ID : {available.ProfessorId} not found");
                }
            }
            catch(Exception ex)
            {
                throw new Exception($"Error while add Professor : {ex.Message}");
            }
        }

        //public async Task<Professor> AddProfessor(Professor professor)
        //{
        //    if(professor == null)
        //    {
        //        throw new ArgumentNullException(nameof(professor), "Details cannot be null.");
        //    }
        //    try
        //    {
        //        professor.Id = Guid.NewGuid();
        //        var studentUser = new User
        //        {
        //            Id = professor.Id,
        //            Name = professor.Name,
        //            UserName = professor.UserName,
        //            Email = professor.Email,
        //            Role = professor.Role,
        //            Password = professor.Password,
        //        };
        //        var userRole = new UserRole
        //        {
        //            Id = Guid.NewGuid(),
        //            UserId = professor.Id,
        //            RoleId = professor.Role,
        //        };
        //        _context.Users.Add(studentUser);
        //        _context.UserRoles.Add(userRole);
        //        await _context.Professors.AddAsync(professor);
        //        await _context.SaveChangesAsync();
        //        //Console.WriteLine("\n\n\n\nProfessor Added Successfully...\n\n\n\n");
        //        return professor;
        //    }catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        throw new Exception($"Error while Add Professor : {ex}");
        //    }
        //}

        public async Task<IEnumerable<User>> GetProfessors()
        {
            var roles = _context.Roles.SingleOrDefault(r => r.Name == "Professor");
            var professors = await _context.Users.Where(s=> s.Role == roles.Id).ToListAsync();
            if( professors == null || !professors.Any())
            {
                return Enumerable.Empty<User>();
            }
            return professors;
        }

    }
}
