﻿using Hospital_System.Models.DTOs;

namespace Hospital_System.Models.Interfaces
{
    public interface IDepartment
    {

        Task<DepartmentDTO> CreateDepartment(DepartmentDTO Department);

        // GET All
        Task<List<DepartmentDTO>> GetDepartments();

        // GET Hotel By Id

        Task<DepartmentDTO> GetDepartment(int DepartmentID);

        // Update
        Task<DepartmentDTO> UpdateDepartment(int id, DepartmentDTO DoctorDTO);

        // Delete 

        Task DeleteDepartment(int id);


    }
}
