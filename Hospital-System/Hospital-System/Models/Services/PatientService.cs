using Hospital_System.Data;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Hospital_System.Models.Services
{
    public class PatientService : IPatient
    {
        private readonly HospitalDbContext _context;

        public PatientService(HospitalDbContext context)
        {
            _context = context;
        }

        public async Task<PatientDTO> Create(PatientDTO Patient)
        {
            var room = await _context.Rooms.FindAsync(Patient.RoomId);
            if (room == null)
            {
                throw new Exception("Room is not exisit");
            }
            var patient = new Patient
            {
                FirstName = Patient.FirstName,
                LastName = Patient.LastName,
                DoB = Patient.DoB,
                Gender = Patient.Gender,
                ContactNumber = Patient.ContactNumber,
                Address = Patient.Address,
                RoomId = Patient.RoomId
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            Patient.Id = patient.Id;

            return Patient;
        }

        public async Task Delete(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Entry(patient).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PatientDTO> GetPatient(int PatientID)
        {
            var patient = await _context.Patients.Select( x => new PatientDTO()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DoB = x.DoB,
                Gender = x.Gender,
                ContactNumber = x.ContactNumber,
                Address = x.Address,
                RoomId = x.RoomId,
                Appointments = x.Appointments.Select(x => new AppointmentDTO()
                {
                    Id = x.Id,
                    DateOfAppointment = x.DateOfAppointment,
                    PatientId = x.PatientId,
                    DoctorId = x.DoctorId
                }).ToList(),
                MedicalReports = x.MedicalReports.Select(x => new MedicalReportDTO()
                {
                    Id = x.Id,
                    ReportDate = x.ReportDate,
                    Description = x.Description,
                    PatientId = x.PatientId,
                    DoctorId = x.DoctorId,
                }).ToList()
            }).FirstOrDefaultAsync(x => x.Id == PatientID);
            return patient;

            //var patient = await _context.Patients.FindAsync(PatientID);
            //if (patient == null)
            //{
            //    return null;
            //}
            //var Patient = new PatientDTO
            //{
            //    Id = patient.Id,
            //    FirstName =patient.FirstName,
            //    LastName=patient.LastName,
            //    DoB = patient.DoB,
            //    Gender = patient.Gender,
            //    ContactNumber = patient.ContactNumber,
            //    Address = patient.Address,
            //    RoomId = patient.RoomId
            //};
            //return Patient;
        }

        public async Task<List<PatientDTO>> GetPatients()
        {
            var patient = await _context.Patients.Select( x => new PatientDTO()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DoB = x.DoB,
                Gender = x.Gender,
                ContactNumber = x.ContactNumber,
                Address = x.Address,
                RoomId = x.RoomId,
                Appointments = x.Appointments.Select(x => new AppointmentDTO()
                {
                    Id = x.Id,
                    DateOfAppointment = x.DateOfAppointment,
                    PatientId = x.PatientId,
                    DoctorId = x.DoctorId
                }).ToList(),
                MedicalReports = x.MedicalReports.Select(x => new MedicalReportDTO()
                {
                    Id = x.Id,
                    ReportDate = x.ReportDate,
                    Description = x.Description,
                    PatientId = x.PatientId,
                    DoctorId = x.DoctorId,
                }).ToList()
            }).ToListAsync();
            return patient;

            //var patient = await _context.Patients.ToListAsync();
            //if(patient.Count==0)
            //{
            //    return null;
            //}
            //var Patient = patient.Select(p => new PatientDTO
            //{
            //    Id = p.Id,
            //    FirstName = p.FirstName,
            //    LastName = p.LastName,
            //    DoB = p.DoB,
            //    Gender = p.Gender,
            //    ContactNumber = p.ContactNumber,
            //    Address = p.Address,
            //    RoomId = p.RoomId
            //}).ToList();
            //return Patient;
        }

        public async Task<PatientDTO> UpdatePatient(int id, PatientDTO patientDTO)
        {
            var Patient = await _context.Patients.FindAsync(id);

            if (Patient != null)
            {
                Patient.Id = patientDTO.Id;
                Patient.FirstName = patientDTO.FirstName;
                Patient.LastName = patientDTO.LastName;
                Patient.DoB = patientDTO.DoB;
                Patient.Gender = patientDTO.Gender;
                Patient.ContactNumber = patientDTO.ContactNumber;
                Patient.Address = patientDTO.Address;
                Patient.RoomId = patientDTO.RoomId;
                await _context.SaveChangesAsync();
                return patientDTO;
            }
            return null;
        }


    }
}
