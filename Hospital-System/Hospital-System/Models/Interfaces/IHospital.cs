using Hospital_System.Models.DTOs;

namespace Hospital_System.Models.Interfaces
{
    public interface IHospital
    {


        Task<Hospital> Create(Hospital hospital);

        // GET All
        Task<List<HospitalDTO>> GetHospitals();


        // Get doctor by id
        Task<HospitalDTO> GetHospital(int HospitalID);


        // Update
        Task<Hospital> UpdateHospital(int id, Hospital hospitalDTO);

        // Delete 

        Task Delete(int id);

    }
}
