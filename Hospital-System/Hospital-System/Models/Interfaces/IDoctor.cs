

using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Doctor;

namespace Hospital_System.Models.Interfaces
{
    public interface IDoctor
    {
        Task<OutDocDTO> Create(InDoctorDTO doctor);

        // GET All
        Task<List<OutDocDTO>> GetDoctors();


        // Get doctor by id
        Task<DoctorDTO> GetDoctor(int DoctorID);


        // Update
        Task<InDoctorDTO> UpdateDoctor(int id, InDoctorDTO DoctorDTO);

        // Delete 

        Task Delete(int id);

        //// Get all appointments of specific doctor
        // Task<DoctorDTO> GetAppointments(int DoctorID);


    }
}
