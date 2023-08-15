
using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Hospital_System.Models.Services
{
    public class AppointmentService : IAppointment
    {
        private readonly HospitalDbContext _context;
        public AppointmentService(HospitalDbContext context)
        {
            _context = context;
        }
        // CREATE Appointment........................................................................
        public async Task<AppointmentDTO> CreateAppointment(AppointmentDTO newAppointmentDTO)
        {
            Appointment appointment = new Appointment
            {
                Id = newAppointmentDTO.Id,
                DateOfAppointment = newAppointmentDTO.DateOfAppointment,
                PatientId = newAppointmentDTO.PatientId,
                DoctorId = newAppointmentDTO.DoctorId,

            };
            _context.Entry(appointment).State = EntityState.Added;

             newAppointmentDTO.Id = appointment.Id;
            await _context.SaveChangesAsync();
            return newAppointmentDTO;
        }
        // Get Appointments........................................................................
        public async Task<List<AppointmentDTO>> GetAppointments()
        {
            var appointment = await _context.Appointments.Select(x => new AppointmentDTO()
            {
                Id = x.Id,
                DateOfAppointment = x.DateOfAppointment,
                PatientId = x.PatientId,
                DoctorId = x.DoctorId,
            }).ToListAsync();
            return appointment;
        }

        // Get Appointment by ID........................................................................
        public async Task<AppointmentDTO> GetAppointment(int id)
        {
            var appointment = await _context.Appointments.Select(x => new AppointmentDTO()
            {
                Id = x.Id,
                DateOfAppointment = x.DateOfAppointment,
                PatientId = x.PatientId,
                DoctorId = x.DoctorId,

            }).FirstOrDefaultAsync(x => x.Id == id);
            return appointment;
        }
        // Update Appointment by ID........................................................................
        public async Task<AppointmentDTO> UpdateAppointment(int id, AppointmentDTO updateAppointmentDTO)
        {
            Appointment appointment = new Appointment
            {
                Id = updateAppointmentDTO.Id,
                DateOfAppointment = updateAppointmentDTO.DateOfAppointment,
                PatientId = (int)updateAppointmentDTO.PatientId,
                DoctorId = (int)updateAppointmentDTO.DoctorId,
            };
            _context.Entry(appointment).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updateAppointmentDTO;
        }
        // Delete Appointment by ID........................................................................
        public async Task DeleteAppointment(int id)
        {
            Appointment appointment = await _context.Appointments.FindAsync(id);
            _context.Entry(appointment).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}