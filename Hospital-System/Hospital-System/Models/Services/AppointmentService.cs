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
        public async Task<OutAppointmentDTO> CreateAppointment(AppointmentDTO newAppointmentDTO)
        {
            // Fetch additional information and check it's existing
            var patientEntity = await _context.Patients.FindAsync(newAppointmentDTO.PatientId);
            var doctorEntity = await _context.Doctors
                .Include(d => d.department)
                .FirstOrDefaultAsync(d => d.Id == newAppointmentDTO.DoctorId);
            if (doctorEntity != null && patientEntity != null)
            {
                Appointment appointment = new Appointment
                {
                    DateOfAppointment = newAppointmentDTO.DateOfAppointment,
                    PatientId = newAppointmentDTO.PatientId,
                    DoctorId = newAppointmentDTO.DoctorId
                };
                _context.Entry(appointment).State = EntityState.Added;
                await _context.SaveChangesAsync();
                var outAppointmentDTO = new OutAppointmentDTO
                {
                    Id = appointment.Id,
                    DateOfAppointment = appointment.DateOfAppointment,
                    PatientId = appointment.PatientId,
                    PatientName = $"{patientEntity.FirstName} {patientEntity.LastName}",
                    DoctorId = appointment.DoctorId,
                    DoctorName = $"{doctorEntity.FirstName} {doctorEntity.LastName}",
                    DepartmentName = doctorEntity.department.DepartmentName // Access DepartmentName property
                };
                return outAppointmentDTO;
            }
            else
            {
                throw new InvalidOperationException("Patient id or Doctor Id is wrong");
            }
        }



        // Get Appointments........................................................................
        public async Task<List<OutAppointmentDTO>> GetAppointments()
        {
            var appointments = await _context.Appointments
                .Include(a => a.patient)
                .Include(a => a.doctor.department)
                .Select(x => new OutAppointmentDTO()
                {
                    Id = x.Id,
                    DateOfAppointment = x.DateOfAppointment,
                    PatientId = x.PatientId,
                    PatientName = $"{x.patient.FirstName} {x.patient.LastName}",
                    DoctorId = x.DoctorId,
                    DoctorName = $"{x.doctor.FirstName} {x.doctor.LastName}",
                    DepartmentName = x.doctor.department.DepartmentName
                }).ToListAsync();
            return appointments;
        }

        // Get Appointment by ID........................................................................
        public async Task<OutAppointmentDTO> GetAppointment(int id)
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