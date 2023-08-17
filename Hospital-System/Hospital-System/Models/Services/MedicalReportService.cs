using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.MedicalReport;
using Hospital_System.Models.DTOs.Medicine;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
namespace Hospital_System.Models.Services
{
    public class MedicalReportService : IMedicalReport
    {
        private readonly HospitalDbContext _context;
        public MedicalReportService(HospitalDbContext context)
        {
            _context = context;
        }
        // CREATE Department........................................................................
        public async Task<OutMedicalReportDTO> CreateMedicalReport(InMedicalReportDTO newMedicalReportDTO)
        {
            var doctor = await _context.Doctors
                .Include(d => d.department)  
                .FirstOrDefaultAsync(d => d.Id == newMedicalReportDTO.DoctorId);

            var patient = await _context.Patients.FindAsync(newMedicalReportDTO.PatientId);
            if (patient != null && doctor != null)
            {
                MedicalReport medicalReport = new MedicalReport
                {
                    ReportDate = newMedicalReportDTO.ReportDate,
                    Description = newMedicalReportDTO.Description,
                    PatientId = newMedicalReportDTO.PatientId,
                    DoctorId = newMedicalReportDTO.DoctorId,
                };

                _context.MedicalReports.Add(medicalReport);
                await _context.SaveChangesAsync();

                OutMedicalReportDTO outMedicalReportDTO = new OutMedicalReportDTO
                {
                    Id = medicalReport.Id,
                    ReportDate = medicalReport.ReportDate,
                    Description = medicalReport.Description,
                    PatientId = medicalReport.PatientId,
                    PatientName = $"{patient.FirstName} {patient.LastName}",
                    DoctorId = medicalReport.DoctorId,
                    DoctorName = $"{doctor.FirstName} {doctor.LastName}",
                    DepartmentName = doctor.department != null ? doctor.department.DepartmentName : string.Empty
                };

                return outMedicalReportDTO;
            }
            else
            {
                throw new ArgumentException("Invalid PatientId or Doctor Id");
            }
        }


        // Get Department........................................................................
        public async Task<List<OutMedicalReportDTO>> GetMedicalReports()
        {
            var medicalReports = await _context.MedicalReports
                .Include(m => m.patient)
                .Include(m => m.doctor.department)
                .Select(x => new OutMedicalReportDTO()
                {
                    Id = x.Id,
                    ReportDate = x.ReportDate,
                    Description = x.Description,
                    PatientId = x.PatientId,
                    PatientName = $"{x.patient.FirstName} {x.patient.LastName}",
                    DoctorId = x.DoctorId,
                    DoctorName = $"{x.doctor.FirstName} {x.doctor.LastName}",
                    DepartmentName = x.doctor.department.DepartmentName,
                }).ToListAsync();

            return medicalReports;
        }





        // Get Department by ID........................................................................
        public async Task<MedicalReportDTO> GetMedicalReport(int id)
        {
            var medicalReport = await _context.MedicalReports.Select(x => new MedicalReportDTO()
            {
                Id = x.Id,
                ReportDate = x.ReportDate,
                Description = x.Description,
                PatientId = x.PatientId,
                DoctorId = x.DoctorId,
                Medicines = x.Medicines.Select(x => new OutMedicineDTO()
                {
                    Id = x.Id,
                    MedicineName = x.MedicineName,
                    Portion = x.Portion,
                }).ToList(),
            }).FirstOrDefaultAsync(x => x.Id == id);
            return medicalReport;
        }
        // Update Department by ID........................................................................
       
        public async Task<OutMedicalReportDTO> UpdateMedicalReport(int id, InMedicalReportDTO updateMedicalReportDTO)
        {
            MedicalReport report = await _context.MedicalReports
                .Include(m => m.patient)
                .Include(m => m.doctor.department)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (report == null)
            {
                throw new ArgumentException($"Medical report with ID {id} not found.");
            }

            report.ReportDate = updateMedicalReportDTO.ReportDate;
            report.Description = updateMedicalReportDTO.Description;

            await _context.SaveChangesAsync();

            var outMedicalReportDTO = new OutMedicalReportDTO
            {
                Id = report.Id,
                ReportDate = report.ReportDate,
                Description = report.Description,
                PatientId = report.PatientId,
                PatientName = $"{report.patient.FirstName} {report.patient.LastName}",
                DoctorId = report.DoctorId,
                DoctorName = $"{report.doctor.FirstName} {report.doctor.LastName}",
                DepartmentName = report.doctor.department.DepartmentName
            };

            return outMedicalReportDTO;
        }

        // Delete Appointment by ID........................................................................
        public async Task DeleteMedicalReport(int id)
        {
            MedicalReport medicalReport = await _context.MedicalReports.FindAsync(id);
            _context.Entry(medicalReport).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}