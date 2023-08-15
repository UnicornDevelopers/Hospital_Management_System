using Hospital_System.Models.DTOs;

namespace Hospital_System.Models.Interfaces
{
    public interface IDoctor
    {
        Task<OutDocDTO> Create(DoctorDTO doctor);

        // GET All
        Task<List<OutDocDTO>> GetDoctors();


        // Get doctor by id
        Task<DoctorDTO> GetDoctor(int DoctorID);


        // Update
        Task<DoctorDTO> UpdateDoctor(int id, DoctorDTO DoctorDTO);

        // Delete 

        Task Delete(int id);

        //// Get all appointments of specific doctor
        // Task<DoctorDTO> GetAppointments(int DoctorID);


    }
}
