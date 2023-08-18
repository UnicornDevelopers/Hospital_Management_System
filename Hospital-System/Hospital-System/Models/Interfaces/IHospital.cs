using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Hospital;

namespace Hospital_System.Models.Interfaces
{
    public interface IHospital
    {


        Task<OutHospitalDTO> Create(OutHospitalDTO hospital);

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
