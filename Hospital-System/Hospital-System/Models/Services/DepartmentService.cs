using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.Doctor;
using Hospital_System.Models.DTOs.Nurse;
using Hospital_System.Models.DTOs.Patient;
using Hospital_System.Models.DTOs.Room;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Hospital_System.Models.Services
{
    public class DepartmentService : IDepartment
    {
    private readonly HospitalDbContext _context;
    public DepartmentService(HospitalDbContext context)
    {
        _context = context;
    }
    // CREATE Department........................................................................
    public async Task<DepartmentDTO> CreateDepartment(DepartmentDTO newDepartmentDTO)
    {
        Department department = new Department
        {
            Id = newDepartmentDTO.Id,
            DepartmentName = newDepartmentDTO.DepartmentName,
            HospitalID = newDepartmentDTO.HospitalID,
        };
        _context.Entry(department).State = EntityState.Added;
        await _context.SaveChangesAsync();
            department.Id = newDepartmentDTO.Id;

            return newDepartmentDTO;
    }
        // Get Department........................................................................
        public async Task<List<OutDepartmentDTO>> GetDepartments()
        {
            var department = await _context.Departments.Select(x => new OutDepartmentDTO()
            {
                Id = x.Id,
                DepartmentName = x.DepartmentName,



            }).ToListAsync();






              
            return department;
        }

        // Get Department by ID........................................................................
        public async Task<DepartmentDTO> GetDepartment(int id)
    {
        var department = await _context.Departments.Select(x => new DepartmentDTO()
        {
            Id = x.Id,
            DepartmentName = x.DepartmentName,
            Rooms = x.Rooms.Select(x => new RoomDTO()
            {
                Id = x.Id,
                RoomNumber = x.RoomNumber,
                RoomAvailability = x.RoomAvailability,
                NumberOfBeds = x.NumberOfBeds,
                Patients = x.Patients.Select(y => new PatientDTO()
                {
                    Id = y.Id,
                    FirstName = y.FirstName,
                    LastName = y.LastName,
                    DoB = y.DoB,
                    Gender = y.Gender,
                    ContactNumber = y.ContactNumber,
                    Address = y.Address
                }).ToList()

            }).ToList(),


            Doctors = x.Doctors.Select(x => new OutDocDTO()
            {
                Id = x.Id,
               FullName = $"{x.FirstName} {x.LastName}",
                Gender = x.Gender,
                ContactNumber = x.ContactNumber,
                Speciality = x.Speciality,
            }).ToList(),




            Nurses = x.Nurses.Select(x => new NurseDTO()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Gender = x.Gender,
                ContactNumber = x.ContactNumber,
            }).ToList()
        }).FirstOrDefaultAsync(x => x.Id == id);
        return department;
    }
    // Update Department by ID........................................................................
    public async Task<DepartmentDTO> UpdateDepartment(int id, DepartmentDTO updateDepartmentDTO)
    {
        Department department = new Department
        {
            Id = updateDepartmentDTO.Id,
            DepartmentName = updateDepartmentDTO.DepartmentName,
        };
        _context.Entry(department).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return updateDepartmentDTO;
    }
        // Delete Appointment by ID........................................................................
        public async Task DeleteDepartment(int id)
    {
            Department department = await _context.Departments.FindAsync(id);
        _context.Entry(department).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }
}
}