using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
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
        public async Task<MedicalReportDTO> CreateMedicalReport(MedicalReportDTO newMedicalReportDTO)
        {
            MedicalReport medicalReport = new MedicalReport
            {
                Id = newMedicalReportDTO.Id,
                ReportDate = newMedicalReportDTO.ReportDate,
                Description = newMedicalReportDTO.Description,
                PatientId = newMedicalReportDTO.PatientId,
                DoctorId = newMedicalReportDTO.DoctorId,

            };
            _context.Entry(medicalReport).State = EntityState.Added;
            newMedicalReportDTO.Id = medicalReport.Id;
            await _context.SaveChangesAsync();
            return newMedicalReportDTO;
        }
        // Get Department........................................................................
        public async Task<List<MedicalReportDTO>> GetMedicalReports()
        {
            var medicalReport = await _context.MedicalReports.Select(x => new MedicalReportDTO()
            {
                Id = x.Id,
                ReportDate = x.ReportDate,
                Description = x.Description,
                PatientId = x.PatientId,
                DoctorId = x.DoctorId,


                Medicines = x.Medicines.Select(x => new MedicineDTO()
                {
                    Id = x.Id,
                    MedicineName = x.MedicineName,
                    Portion = x.Portion,
                }).ToList(),


            }).ToListAsync();
            return medicalReport;
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


                Medicines = x.Medicines.Select(x => new MedicineDTO()
                {
                    Id = x.Id,
                    MedicineName = x.MedicineName,
                    Portion = x.Portion,
                }).ToList(),


            }).FirstOrDefaultAsync(x => x.Id == id);
            return medicalReport;
        }
        // Update Department by ID........................................................................
        public async Task<MedicalReportDTO> UpdateMedicalReport(int id, MedicalReportDTO updateMedicalReportDTO)
        {
            MedicalReport medicalReport = new MedicalReport
            {
                Id = updateMedicalReportDTO.Id,
                ReportDate = updateMedicalReportDTO.ReportDate,
                Description = updateMedicalReportDTO.Description,
            };
            _context.Entry(medicalReport).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updateMedicalReportDTO;
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