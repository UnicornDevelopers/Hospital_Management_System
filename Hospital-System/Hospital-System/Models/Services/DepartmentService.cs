﻿using Hospital_System.Data;
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
    public async Task<InDepartmentDTO> CreateDepartment(InDepartmentDTO newDepartmentDTO)
    {
        Department department = new Department
        {
            DepartmentName = newDepartmentDTO.DepartmentName,
            HospitalID = newDepartmentDTO.HospitalID,
        };
        _context.Entry(department).State = EntityState.Added;
        await _context.SaveChangesAsync();
            newDepartmentDTO.Id = department.Id;

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
            var department = await _context.Departments
                .Where(x => x.Id == id)
                .Select(x => new DepartmentDTO()
                {
                    Id = x.Id,
                    DepartmentName = x.DepartmentName,

                    Rooms = x.Rooms.Select(room => new OutRoomDTO
                    {
                        Id = room.Id,
                        RoomNumber = room.RoomNumber,
                        RoomAvailability = room.RoomAvailability,
                        NumberOfBeds = room.NumberOfBeds
                    }).ToList(),

                    Doctors = x.Doctors.Select(x => new OutDocDTO()
                    {
                        Id = x.Id,
                        FullName = $"{x.FirstName} {x.LastName}",
                        Gender = x.Gender,
                        ContactNumber = x.ContactNumber,
                        Speciality = x.Speciality,
                    }).ToList(),

                    Nurses = x.Nurses.Select(x => new InNurseDTO()
                    {
                        Id = x.Id,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Gender = x.Gender,
                        ContactNumber = x.ContactNumber,
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return department;
        }


        // Update Department by ID........................................................................
        public async Task<OutDepartmentDTO> UpdateDepartment(int id, OutDepartmentDTO updateDepartmentDTO)
        {
            var existingDepartment = await _context.Departments.FindAsync(id);

            if (existingDepartment == null)
            {
                throw new InvalidOperationException($"Department with ID {id} not found.");
            }

            existingDepartment.DepartmentName = updateDepartmentDTO.DepartmentName;

            _context.Entry(existingDepartment).State = EntityState.Modified;
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



        public async Task<List<OutDocDTO>> GetDoctorsInDepartment(int departmentId)
        {
            var doctors = await _context.Doctors
                .Where(d => d.DepartmentId == departmentId)
                .Select(d => new OutDocDTO()
                {
                    Id = d.Id,
                    FullName = $"{d.FirstName} {d.LastName}",
                    Gender = d.Gender,
                    ContactNumber = d.ContactNumber,
                    Speciality = d.Speciality,
                })
                .ToListAsync();

            return doctors;
        }







    }
}