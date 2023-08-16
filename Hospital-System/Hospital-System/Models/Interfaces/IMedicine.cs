using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Medicine;

namespace Hospital_System.Models.Interfaces
{
    public interface IMedicine
    {
        Task<OutMedicineDTO> CreateMedicine(OutMedicineDTO Medicine);

        // GET All
        Task<List<OutMedicineDTO>> GetMedicines();

        // GET Hotel By Id

        Task<OutMedicineDTO> GetMedicine(int MedicineID);

        // Update
        Task<OutMedicineDTO> UpdateMedicine(int id, OutMedicineDTO medicineDto);

        // Delete 

        Task DeleteMedicine(int id);

    }
}
