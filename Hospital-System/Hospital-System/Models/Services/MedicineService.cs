
using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Medicine;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;


namespace Hospital_System.Models.Services
{
    public class MedicineService : IMedicine
    {
        private readonly HospitalDbContext _context;
        public MedicineService(HospitalDbContext context)
        {
            _context = context;
        }
        // CREATE Medicine........................................................................
        public async Task<OutMedicineDTO> CreateMedicine(OutMedicineDTO newMedicineDTO)
        {
            Medicine medicine = new Medicine
            {
                MedicineName = newMedicineDTO.MedicineName,
                Portion = newMedicineDTO.Portion,
                MedicalReportId= newMedicineDTO.Id
            };
            _context.Entry(medicine).State = EntityState.Added;
            await _context.SaveChangesAsync();
            newMedicineDTO.Id = medicine.Id;
            return newMedicineDTO;
        }
        // Get Medicine........................................................................
        public async Task<List<OutMedicineDTO>> GetMedicines()
        {
            var medicines = await _context.Medicines.Select(x => new OutMedicineDTO()
            {
                Id = x.Id,
                MedicineName = x.MedicineName,
                Portion = x.Portion,
            }).ToListAsync();
            return medicines;
        }
        // Get Medicine by ID........................................................................
        public async Task<OutMedicineDTO> GetMedicine(int id)
        {
            var medicine = await _context.Medicines.Select(x => new OutMedicineDTO()
            {
                Id = x.Id,
                MedicineName = x.MedicineName,
                Portion = x.Portion,
            }).FirstOrDefaultAsync(x => x.Id == id);
            return medicine;
        }
        // Update Medicine by ID........................................................................
        public async Task<OutMedicineDTO> UpdateMedicine(int id, OutMedicineDTO updateMedicineDTO)
        {
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
            {
                return null;
            }
            medicine.MedicineName = updateMedicineDTO.MedicineName;
            medicine.Portion = updateMedicineDTO.Portion;
            _context.Entry(medicine).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updateMedicineDTO;
        }

        // Delete Medicine by ID........................................................................
        public async Task DeleteMedicine(int id)
        {
            Medicine medicine = await _context.Medicines.FindAsync(id);
            _context.Entry(medicine).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}


