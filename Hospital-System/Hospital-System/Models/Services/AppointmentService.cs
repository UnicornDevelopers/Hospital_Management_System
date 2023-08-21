using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Appointment;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Hospital_System.Models.Services
{
    /// <summary>
    /// Service class for managing appointments.
    /// </summary>
    public class AppointmentService : IAppointment
    {
        private readonly HospitalDbContext _context;

        public AppointmentService(HospitalDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new appointment.
        /// </summary>
        /// <param name="newAppointmentDTO">The appointment data to be created.</param>
        /// <returns>The created appointment's information.</returns>
        public async Task<OutAppointmentDTO> CreateAppointment(InAppoinmentDTO newAppointmentDTO)
        {
            // Fetch additional information and check its existence
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
                    DepartmentName = doctorEntity.department.DepartmentName
                };
                return outAppointmentDTO;
            }
            else
            {
                throw new InvalidOperationException("Patient ID or Doctor ID is wrong");
            }
        }

        /// <summary>
        /// Retrieves a list of all appointments.
        /// </summary>
        /// <returns>A list of all appointments.</returns>
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

        /// <summary>
        /// Retrieves detailed information about a specific appointment.
        /// </summary>
        /// <param name="id">The ID of the appointment to retrieve.</param>
        /// <returns>Detailed information about the requested appointment.</returns>
        public async Task<OutAppointmentDTO> GetAppointment(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.doctor.department)
                .Select(x => new OutAppointmentDTO()
                {
                    Id = x.Id,
                    DateOfAppointment = x.DateOfAppointment,
                    PatientId = x.PatientId,
                    DoctorId = x.DoctorId,
                    PatientName = $"{x.patient.FirstName} {x.patient.LastName}",
                    DoctorName = $"{x.doctor.FirstName} {x.doctor.LastName}",
                    DepartmentName = x.doctor.department.DepartmentName,
                })
                .FirstOrDefaultAsync(x => x.Id == id);

            return appointment;
        }

        /// <summary>
        /// Updates the details of an existing appointment.
        /// </summary>
        /// <param name="id">The ID of the appointment to update.</param>
        /// <param name="updateAppointmentDTO">The updated appointment data.</param>
        /// <returns>The updated appointment's information.</returns>
        public async Task<InAppoinmentDTO> UpdateAppointment(int id, InAppoinmentDTO updateAppointmentDTO)
        {
            var existingAppointment = await _context.Appointments.FindAsync(id);

            if (existingAppointment == null)
            {
                throw new ArgumentException($"Appointment with ID {id} not found.");
            }

            var patientExists = await _context.Patients.AnyAsync(p => p.Id == updateAppointmentDTO.PatientId);
            if (!patientExists)
            {
                throw new ArgumentException($"Patient with ID {updateAppointmentDTO.PatientId} not found.");
            }

            var doctorExists = await _context.Doctors.AnyAsync(d => d.Id == updateAppointmentDTO.DoctorId);
            if (!doctorExists)
            {
                throw new ArgumentException($"Doctor with ID {updateAppointmentDTO.DoctorId} not found.");
            }

            existingAppointment.DateOfAppointment = updateAppointmentDTO.DateOfAppointment;
            existingAppointment.PatientId = updateAppointmentDTO.PatientId;
            existingAppointment.DoctorId = updateAppointmentDTO.DoctorId;

            await _context.SaveChangesAsync();

            return updateAppointmentDTO;
        }

        /// <summary>
        /// Deletes an appointment by its ID.
        /// </summary>
        /// <param name="id">The ID of the appointment to delete.</param>
        public async Task DeleteAppointment(int id)
        {
            Appointment appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Entry(appointment).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"The Appointment ID {id} does not exist.");
            }
        }
    }
}
