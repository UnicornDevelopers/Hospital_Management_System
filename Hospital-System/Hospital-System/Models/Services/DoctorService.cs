
using Hospital_System.Data;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.AppointmentDTO;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.Doctor;
using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Hospital_System.Models.Services
{
    public class DoctorService : IDoctor

    {
        private readonly HospitalDbContext _context;
        public DoctorService(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<OutDocDTO> Create(InDoctorDTO doctor)
        {
            var existingDep = await _context.Departments.FindAsync(doctor.DepartmentId);

            if (existingDep != null)
            {
                Doctor doctorEntity = new Doctor()
                {
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName,
                    Gender = doctor.Gender,
                    ContactNumber = doctor.ContactNumber,
                    Speciality = doctor.Speciality,
                    DepartmentId = doctor.DepartmentId
                };

                _context.Doctors.Add(doctorEntity);
                await _context.SaveChangesAsync();

                OutDocDTO outputDoc = new OutDocDTO()
                {
                    Id = doctorEntity.Id,
                    FullName = $"{doctor.FirstName} {doctor.LastName}",
                    Gender = doctorEntity.Gender,
                    ContactNumber = doctor.ContactNumber,
                    Speciality = doctor.Speciality,
                    DepartmentName = existingDep.DepartmentName
                };

                return outputDoc;
            }
            else
            {
                throw new InvalidOperationException($"Department with ID {doctor.DepartmentId} not found.");
            }
        }


        public async Task Delete(int id)
        {
            Doctor existingDoc = await _context.Doctors.FindAsync(id);
            if (existingDoc != null)
            {
                _context.Entry(existingDoc).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }

            else
            {
                throw new InvalidOperationException("The Doctor Id is not exist.");
            }
        }

        public async Task<DoctorDTO> GetDoctor(int DoctorID)
        {
            var doctor = await _context.Doctors
                .Include(d => d.Appointments)
                .Include(d => d.medicalReports)
                .Include(d => d.department)
                .FirstOrDefaultAsync(d => d.Id == DoctorID);

            if (doctor == null)
            {
                throw new InvalidOperationException($"Doctor with ID {DoctorID} not found.");
            }

            var doctorDTO = new DoctorDTO
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Gender = doctor.Gender,
                ContactNumber = doctor.ContactNumber,
                Speciality = doctor.Speciality,
                DepartmentId = doctor.DepartmentId,
                Appointments = doctor.Appointments?.Select(a => new OutAppointmentDTO
                {
                    Id = a.Id,
                    DateOfAppointment = a.DateOfAppointment,
                    PatientId = a.PatientId,
                    DoctorId = a.DoctorId,
                }).ToList(),
                medicalReports = doctor.medicalReports?.Select(r => new OutMedicalReportDTO
                {
                    Id = r.Id,
                    ReportDate = r.ReportDate,
                    Description = r.Description,
                    PatientId = r.PatientId,
                    DoctorId = r.DoctorId,
                }).ToList(),
                department = new OutDepartmentDTO
                {
                    Id = doctor.department.Id,
                    DepartmentName = doctor.department.DepartmentName,
                }
            };

            return doctorDTO;
        }



        public async Task<List<OutDocDTO>> GetDoctors()
        {

            var doctors = await _context.Doctors
                .Include(d => d.department)
                .Select(d => new OutDocDTO
                {
                    Id = d.Id,
                    FullName = $"{d.FirstName} {d.LastName}",
                    Gender = d.Gender,
                    ContactNumber = d.ContactNumber,
                    Speciality = d.Speciality,
                    DepartmentName = d.department.DepartmentName
                })
                .ToListAsync();

            return doctors;


        }

        public async Task<InDoctorDTO> UpdateDoctor(int id, InDoctorDTO doctorDTO)
        {
            var existingDoctor = await _context.Doctors.FindAsync(id);

            if (existingDoctor == null)
            {
                throw new InvalidOperationException($"Doctor with ID {id} not found.");
            }
            var existingDep = await _context.Departments.FindAsync(doctorDTO.DepartmentId);

            if (existingDep == null)
            {
                throw new InvalidOperationException($"invaild Department with ID {doctorDTO.DepartmentId} not found.");
            }

            existingDoctor.FirstName = doctorDTO.FirstName;
            existingDoctor.LastName = doctorDTO.LastName;
            existingDoctor.Gender = doctorDTO.Gender;
            existingDoctor.ContactNumber = doctorDTO.ContactNumber;
            existingDoctor.Speciality = doctorDTO.Speciality;
            existingDoctor.DepartmentId = doctorDTO.DepartmentId;

            await _context.SaveChangesAsync();

            var updatedDoctorDTO = new InDoctorDTO
            {
                Id = existingDoctor.Id,
                FirstName = existingDoctor.FirstName,
                LastName = existingDoctor.LastName,
                Gender = existingDoctor.Gender,
                ContactNumber = existingDoctor.ContactNumber,
                Speciality = existingDoctor.Speciality,
                DepartmentId = existingDoctor.DepartmentId,
            };

            return updatedDoctorDTO;
        }

    }
}

