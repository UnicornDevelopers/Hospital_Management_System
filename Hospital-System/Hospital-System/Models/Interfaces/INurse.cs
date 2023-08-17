using Hospital_System.Models.DTOs.Nurse;

namespace Hospital_System.Models.Interfaces
{
    public interface INurse
    {

        Task<NurseDTO> Create(InNurseDTO Nurse);

        // GET All
        Task<List<InNurseDTO>> GetNurses();

        // GET Hotel By Id

        Task<NurseDTO> GetNurse(int NurseID);

        // Update
        Task<InNurseDTO> UpdateNurse(int id, InNurseDTO nurseDto);

        // Delete 

        Task Delete(int id);

    }
}
