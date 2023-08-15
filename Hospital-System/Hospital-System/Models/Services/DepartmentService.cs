using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
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
        };
        _context.Entry(department).State = EntityState.Added;
        await _context.SaveChangesAsync();
        return newDepartmentDTO;
    }
        // Get Department........................................................................
        public async Task<List<DepartmentDTO>> GetDepartments()
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


                Doctors = x.Doctors.Select(x => new DoctorDTO()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
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


            Doctors = x.Doctors.Select(x => new DoctorDTO()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
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