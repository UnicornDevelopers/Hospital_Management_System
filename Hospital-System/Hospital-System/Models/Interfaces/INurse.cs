using Hospital_System.Models.DTOs.Nurse;

namespace Hospital_System.Models.Interfaces
{
    public interface INurse
    {

        Task<NurseDTO> Create(InNurseDTO Nurse);

        // GET All
        Task<List<NurseDTO>> GetNurses();

        // GET Hotel By Id

        Task<NurseDTO> GetNurse(int NurseID);

        // Update
        Task<NurseDTO> UpdateNurse(int id, NurseDTO nurseDto);

        // Delete 

        Task Delete(int id);

    }
}
