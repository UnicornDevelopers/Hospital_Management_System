using Hospital_System.Data;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.AppointmentDTO;
using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.DTOs.Patient;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Linq;
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

        public async Task<OutPatientDTO> Create(InPatientDTO Patient)
        {
            if (Patient.RoomId != null)
            {
                var room = await _context.Rooms.Include(p => p.Patients).FirstOrDefaultAsync(a => a.Id == Patient.RoomId);
                if (room == null)
                {
                    throw new Exception("Room is not exisit");
                }
                if (room.NumberOfBeds == room.Patients.Count)
                {
                    room.RoomAvailability = false;
                }
                var dep = await _context.Departments.FindAsync(room.DepartmentId);
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
                if (room.RoomAvailability)
                {
                    _context.Patients.Add(patient);
                    await _context.SaveChangesAsync();
                    Patient.Id = patient.Id;
                    var patientDTO = new OutPatientDTO
                    {
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        DoB = patient.DoB,
                        Gender = patient.Gender,
                        ContactNumber = patient.ContactNumber,
                        Address = patient.Address,
                        RoomId = patient.RoomId,
                        
                    };
                    return patientDTO;
                }
            }
            else
            {
                var patient = new Patient
                {
                    FirstName = Patient.FirstName,
                    LastName = Patient.LastName,
                    DoB = Patient.DoB,
                    Gender = Patient.Gender,
                    ContactNumber = Patient.ContactNumber,
                    Address = Patient.Address
                };
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
                Patient.Id = patient.Id;
                var patientDTO = new OutPatientDTO
                {
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    DoB = patient.DoB,
                    Gender = patient.Gender,
                    ContactNumber = patient.ContactNumber,
                    Address = patient.Address
                };
                return patientDTO;
            }
            return null;
        }        /* public async Task<OutPatientDTO> AddPatientToRoom(InPatientDTO Patient)
        {
            var patientEntity = await _context.Patients.FindAsync(Patient.Id);

        }*/


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
            var patient = await _context.Patients
                .Include(p => p.Appointments)
                .Include(p => p.MedicalReports)
                    .ThenInclude(mr => mr.doctor)
                        .ThenInclude(d => d.department)
                .Select(x => new PatientDTO()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DoB = x.DoB,
                    Gender = x.Gender,
                    ContactNumber = x.ContactNumber,
                    Address = x.Address,
                    RoomId = x.RoomId,
                    Appointments = x.Appointments.Select(a => new OutAppointmentDTO()
                    {
                        Id = a.Id,
                        DateOfAppointment = a.DateOfAppointment,
                        PatientId = a.PatientId,
                        DoctorId = a.DoctorId,
                        DoctorName = $"{a.doctor.FirstName} {a.doctor.LastName}",
                        DepartmentName = a.doctor.department.DepartmentName
                    }).ToList(),
                    MedicalReports = null
                })
                .FirstOrDefaultAsync(x => x.Id == PatientID);

            return patient;
        }




        public async Task<List<PatientDTO>> GetPatients()
        {
            var patients = await _context.Patients
                .Select(x => new PatientDTO()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DoB = x.DoB,
                    Gender = x.Gender,
                    ContactNumber = x.ContactNumber,
                    Address = x.Address,
                    RoomId = x.RoomId,
                    Appointments = null,
                    
                    MedicalReports = x.MedicalReports.Select(mr => new MedicalReportDTO()
                    {
                        Id = mr.Id,
                        ReportDate = mr.ReportDate,
                        Description = mr.Description,
                        PatientId = mr.PatientId,
                        DoctorId = mr.DoctorId,
                    }).ToList()
                }).ToListAsync();

            return patients;
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
