using Hospital_System.Models.DTOs.Patient;

namespace Hospital_System.Models.Interfaces
{
    public interface IPatient
    {

        Task<OutPatientDTO> Create(InPatientDTO Patient);

        // GET All
        Task<List<PatientDTO>> GetPatients();

        // GET Hotel By Id

        Task<PatientDTO> GetPatient(int PatientID);

        // Update
        Task<PatientDTO> UpdatePatient(int id, PatientDTO DoctorDTO);

        // Delete 

        Task Delete(int id);

    }
}
