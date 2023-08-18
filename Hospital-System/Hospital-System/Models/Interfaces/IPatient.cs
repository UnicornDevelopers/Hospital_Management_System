using Hospital_System.Models.DTOs.Patient;
namespace Hospital_System.Models.Interfaces
{
    public interface IPatient
    {
        Task<OutPatientDTO> Create(InPatientDTO Patient);
        // GET All
        Task<List<OutPatientDTO>> GetPatients();
        // GET Hotel By Id
        Task<PatientDTO> GetPatient(int PatientID);
        // Update
        Task<OutPatientDTO> UpdatePatient(int id, InPatientDTO DoctorDTO);
        // Delete
        Task Delete(int id);
    }
}