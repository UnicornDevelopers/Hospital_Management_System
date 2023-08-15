
using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
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
        public async Task<MedicineDTO> CreateMedicine(MedicineDTO newMedicineDTO)
        {
            Medicine medicine = new Medicine
            {
                Id = newMedicineDTO.Id,
                MedicineName = newMedicineDTO.MedicineName,
                Portion = newMedicineDTO.Portion,
            };
            _context.Entry(medicine).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return newMedicineDTO;
        }
        // Get Medicine........................................................................
        public async Task<List<MedicineDTO>> GetMedicines()
        {
            var medicine = await _context.Medicines.Select(x => new MedicineDTO()
            {
                Id = x.Id,
                MedicineName = x.MedicineName,
                Portion = x.Portion,
            }).ToListAsync();
            return medicine;
        }
        // Get Medicine by ID........................................................................
        public async Task<MedicineDTO> GetMedicine(int id)
        {
            var medicine = await _context.Medicines.Select(x => new MedicineDTO()
            {
                Id = x.Id,
                MedicineName = x.MedicineName,
                Portion = x.Portion,
            }).FirstOrDefaultAsync(x => x.Id == id);
            return medicine;
        }
        // Update Medicine by ID........................................................................
        public async Task<MedicineDTO> UpdateMedicine(int id, MedicineDTO updateMedicineDTO)
        {
            Medicine medicine = new Medicine
            {
                Id = updateMedicineDTO.Id,
                MedicineName = updateMedicineDTO.MedicineName,
                Portion = updateMedicineDTO.Portion,
            };
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


