using Hospital_System.Models.DTOs;

namespace Hospital_System.Models.Interfaces
{
    public interface IMedicine
    {
        Task<MedicineDTO> CreateMedicine(MedicineDTO Medicine);

        // GET All
        Task<List<MedicineDTO>> GetMedicines();

        // GET Hotel By Id

        Task<MedicineDTO> GetMedicine(int MedicineID);

        // Update
        Task<MedicineDTO> UpdateMedicine(int id, MedicineDTO medicineDto);

        // Delete 

        Task DeleteMedicine(int id);

    }
}
