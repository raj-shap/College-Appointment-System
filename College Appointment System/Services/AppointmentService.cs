using College_Appointment_System.Context;
using College_Appointment_System.Interfaces;
using College_Appointment_System.Models;
using Microsoft.EntityFrameworkCore;

namespace College_Appointment_System.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _context;
        public AppointmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment> AddAppointment(Appointment appointment)
        {
            if(appointment == null)
            {
                throw new ArgumentNullException(nameof(appointment),"Details cannot be null");
            }
            try
            {
                appointment.Id = Guid.NewGuid();
                var professor = await _context.Users.SingleOrDefaultAsync(p => p.Id == appointment.ProfessorId);
                var student = await _context.Users.SingleOrDefaultAsync(s => s.Id == appointment.StudentId);
                var availability = await _context.Availability.SingleOrDefaultAsync(a => a.Id == appointment.AvailabilityId);
                if (professor == null || student == null || availability == null)
                {
                    throw new Exception("Data Not Found");
                }
                
                if(availability.IsBooked == true)
                {
                    throw new Exception("Appointment not available.");
                }
                availability.IsBooked = true;
                _context.Availability.Update(availability);
                await _context.Appointments.AddAsync(appointment);
                await _context.SaveChangesAsync();
                return appointment;
            }
            catch(Exception ex)
            {
                throw new Exception($"Error while BookAppointment ${ex.Message}");
            }
        }

        public async Task<IEnumerable<Appointment>> GetAppointments(Guid StudentId)
        {
            try
            {
                var appointments = await _context.Appointments.Where(a=>a.StudentId == StudentId).ToListAsync();
                //var appointmentById = new List<Appointment>();
                if(appointments == null)
                {
                    throw new Exception("Data Not Found");
                }
                //foreach(var appointment in appointments)
                //{
                //    if(appointment.StudentId == StudentId)
                //    {
                //        appointmentById.Add(appointment);
                //    }
                //}
                return appointments;
            }
            catch(Exception ex)
            {
                throw new Exception($"Error while getting Appointments");
            }
        }

        public async Task<IEnumerable<Availability>> GetBookedAvailabilities(Guid ProfessorId)
        {
            try
            {
                var availability = await _context.Availability.ToListAsync();
                var availableAppointment = new List<Availability>();
                if(availability == null)
                {
                    throw new Exception("Data Not Found");
                }
                foreach(var available in availability)
                {
                    if(available.IsBooked != false)
                    {
                        availableAppointment.Add(available);
                    }
                }
                return availableAppointment;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while getting Availabilities : {ex.Message}");
            }
        }
        
        public async Task<IEnumerable<Availability>> GetNotBookedAvailabilities(Guid ProfessorId)
        {
            try
            {
                var availability = await _context.Availability.Where(a => (a.ProfessorId == ProfessorId) && (a.IsBooked != true)).ToListAsync();
                //var availableAppointment = new List<Availability>();
                if(availability == null)
                {
                    throw new Exception("Data Not Found");
                }
                //foreach(var available in availability)
                //{
                //    if(available.IsBooked != true)
                //    {
                //        availableAppointment.Add(available);
                //    }
                //}
                return availability;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while getting Availabilities : {ex.Message}");
            }
        }
    }
}
