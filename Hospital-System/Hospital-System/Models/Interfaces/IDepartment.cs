using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Department;

namespace Hospital_System.Models.Interfaces
{
    public interface IDepartment
    {

        Task<InDepartmentDTO> CreateDepartment(InDepartmentDTO Department);

        // GET All
        Task<List<OutDepartmentDTO>> GetDepartments();

        // GET Hotel By Id

        Task<DepartmentDTO> GetDepartment(int DepartmentID);

        // Update
        Task<OutDepartmentDTO> UpdateDepartment(int id, OutDepartmentDTO DoctorDTO);

        // Delete 

        Task DeleteDepartment(int id);


    }
}
