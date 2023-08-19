using Hospital_System.Data;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.AppointmentDTO;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.DTOs.Patient;
using Hospital_System.Models.DTOs.Room;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.Arm;
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
                if (room.NumberOfBeds == room.Patients!.Count)
                {
                    room.RoomAvailability = false;
                }
                else
                {
                    room.RoomAvailability = true;
                }
                var dep = await _context.Departments.FindAsync(room.DepartmentId);
                if (room.RoomAvailability)
                {
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
                    var patientDTO = new OutPatientDTO
                    {
                        Id = patient.Id,
                        FirstName = patient.FirstName,
                        LastName = patient.LastName,
                        DoB = patient.DoB,
                        Gender = patient.Gender,
                        ContactNumber = patient.ContactNumber,
                        Address = patient.Address,
                        RoomId = patient.RoomId,
                        DepartmentName = dep!.DepartmentName,
                        RoomNumber = room.RoomNumber
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
                var patientDTO = new OutPatientDTO
                {
                    Id = patient.Id,
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
            var Patient = await _context.Patients
                .Include(r => r.Rooms)
                .Include(p => p.Appointments)
                .Include(p => p.MedicalReports)
                    .ThenInclude(mr => mr.doctor)
                        .ThenInclude(d => d.department)
                        .FirstOrDefaultAsync(f => f.Id == PatientID);
            if (Patient == null)
            {
                return null;
            }
            var Department = new OutDepartmentDTO
            {
                Id = Patient.Rooms!.department!.Id,
                DepartmentName = Patient.Rooms.department.DepartmentName
            };
            var Room = new RoomPatient
            {
                Id = Patient.Rooms.Id,
                RoomNumber = Patient.Rooms.RoomNumber,
                RoomAvailability = Patient.Rooms.RoomAvailability,
                NumberOfBeds = Patient.Rooms.NumberOfBeds,
                DepartmentId = Patient.Rooms.DepartmentId,
                department = Department
            };
            var patient = new PatientDTO
            {
                Id = Patient.Id,
                FirstName = Patient.FirstName,
                LastName = Patient.LastName,
                DoB = Patient.DoB,
                Gender = Patient.Gender,
                ContactNumber = Patient.ContactNumber,
                Address = Patient.Address,
                RoomId = Patient.RoomId,
                Rooms = Room,
                Appointments = Patient.Appointments.Select(a => new OutAppointmentDTO()
                {
                    Id = a.Id,
                    DateOfAppointment = a.DateOfAppointment,
                    PatientId = a.PatientId,
                    PatientName = $"{a.patient.FirstName} {a.patient.LastName}",
                    DoctorId = a.DoctorId,
                    DoctorName = $"{a.doctor.FirstName} {a.doctor.LastName}",
                    DepartmentName = a.doctor.department.DepartmentName
                }).ToList(),
                MedicalReports = Patient.MedicalReports.Select(m => new OutMedicalReportDTO()
                {
                    Id = m.Id,
                    ReportDate = m.ReportDate,
                    Description = m.Description,
                    PatientId = m.PatientId,
                    PatientName = $"{m.patient!.FirstName} {m.patient.LastName}",
                    DoctorId = m.DoctorId,
                    DoctorName = $"{m.doctor!.FirstName} {m.doctor.LastName}",
                    DepartmentName = m.doctor.department.DepartmentName
                }).ToList()
            };
            return patient;
        }
        public async Task<List<OutPatientDTO>> GetPatients()
        {
            var patients = await _context.Patients.Include(r => r.Rooms).ThenInclude(d => d.department)
                .Select(x => new OutPatientDTO()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DoB = x.DoB,
                    Gender = x.Gender,
                    ContactNumber = x.ContactNumber,
                    Address = x.Address,
                    RoomId = x.RoomId,
                    RoomNumber = x.Rooms!.RoomNumber,
                    DepartmentName = x.Rooms.department!.DepartmentName
                }).ToListAsync();
            return patients;
        }
        public async Task<OutPatientDTO> UpdatePatient(int id, InPatientDTO patientDTO)
        {
            var Patient = await _context.Patients.FindAsync(id);
            if (Patient != null)
            {
                Patient.FirstName = patientDTO.FirstName;
                Patient.LastName = patientDTO.LastName;
                Patient.DoB = patientDTO.DoB;
                Patient.Gender = patientDTO.Gender;
                Patient.ContactNumber = patientDTO.ContactNumber;
                Patient.Address = patientDTO.Address;
                Patient.RoomId = patientDTO.RoomId;
                if (patientDTO.RoomId != null)
                {
                    var room = await _context.Rooms.Include(p => p.Patients).FirstOrDefaultAsync(a => a.Id == patientDTO.RoomId);
                    if (room == null)
                    {
                        throw new Exception("Room is not exisit");
                    }
                    if (room.NumberOfBeds + 1 == room.Patients!.Count)
                    {
                        room.RoomAvailability = false;
                    }
                    else
                    {
                        room.RoomAvailability = true;
                    }
                    var dep = await _context.Departments.FindAsync(room.DepartmentId);
                    if (room.RoomAvailability)
                    {
                        var patient = new OutPatientDTO
                        {
                            FirstName = Patient.FirstName,
                            LastName = Patient.LastName,
                            DoB = Patient.DoB,
                            Gender = Patient.Gender,
                            ContactNumber = Patient.ContactNumber,
                            Address = Patient.Address,
                            RoomId = Patient.RoomId,
                            RoomNumber = room.RoomNumber,
                            DepartmentName = dep.DepartmentName
                        };
                        await _context.SaveChangesAsync();
                        return patient;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    var patient = new OutPatientDTO
                    {
                        FirstName = Patient.FirstName,
                        LastName = Patient.LastName,
                        DoB = Patient.DoB,
                        Gender = Patient.Gender,
                        ContactNumber = Patient.ContactNumber,
                        Address = Patient.Address
                    };
                    await _context.SaveChangesAsync();
                    return patient;
                }
            }
            return null;
        }
    }
}