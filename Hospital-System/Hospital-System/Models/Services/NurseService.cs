
using System;
using Hospital_System.Data;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Department;
using Hospital_System.Models.DTOs.Doctor;
using Hospital_System.Models.DTOs.Nurse;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Hospital_System.Models.Nurse;

namespace Hospital_System.Models.Services
{
    public class NurseService : INurse
    {

        private readonly HospitalDbContext _context;

        public NurseService(HospitalDbContext context)
        {
            _context = context;
        }





        public async Task<NurseDTO> Create(InNurseDTO nurseDTO)
        {
            var departmentExists = await _context.Departments.AnyAsync(d => d.Id == nurseDTO.DepartmentId);
            if (!departmentExists)
            {
                throw new InvalidOperationException($"Department with ID {nurseDTO.DepartmentId} does not exist.");
            }
            // Create a new Nurse entity

            var nurse = new Nurse
            {
                Id = nurseDTO.Id,
                FirstName = nurseDTO.FirstName,
                LastName = nurseDTO.LastName,
                Gender = nurseDTO.Gender,
                ContactNumber = nurseDTO.ContactNumber,
                shift = nurseDTO.Shift,
                DepartmentId = nurseDTO.DepartmentId

            };

            _context.Entry(nurse).State = EntityState.Added;

            await _context.SaveChangesAsync();

            // Retrieve the added Nurse by ID
            NurseDTO addedNurse = await GetNurse(nurse.Id);

            return addedNurse;
        }





        public async Task<NurseDTO> GetNurse(int nurseID)
        {
            // Find the Nurse by ID and include the Department
            var nurse = await _context.Nurses
                .Include(n => n.department)
                .FirstOrDefaultAsync(n => n.Id == nurseID);

            if (nurse == null)
                return null;

            var nurseDTO = new NurseDTO
            {
                Id = nurse.Id,
                FirstName = nurse.FirstName,
                LastName = nurse.LastName,
                Gender = nurse.Gender,
                ContactNumber = nurse.ContactNumber,
                Shift = nurse.shift,
                DepartmentId = nurse.DepartmentId,
                department = new OutDepartmentDTO
                {
                    Id = nurse.department.Id,
                    DepartmentName = nurse.department.DepartmentName
                }
            };

            return nurseDTO;
        }





        public async Task<List<InNurseDTO>> GetNurses()
        {
            var nurses = await _context.Nurses.Include(nurse => nurse.department).ToListAsync();

            // Map Nurses entities to NurseDTOs
            var nursesDto = nurses.Select(n => new InNurseDTO
            {
                Id = n.Id,
                FirstName = n.FirstName,
                LastName = n.LastName,
                Gender = n.Gender,
                ContactNumber = n.ContactNumber,
                Shift = n.shift,
                DepartmentId = n.DepartmentId
            }
            ).ToList();

            return nursesDto;

        }






        public async Task<InNurseDTO> UpdateNurse(int id, InNurseDTO nurseDto)
        {


            var nurse = await _context.Nurses.FindAsync(id);

            if (nurse == null)
                throw new InvalidOperationException($"invaild nurse with ID {nurseDto.Id} not found.");

            var departmentExists = await _context.Departments.FindAsync(nurseDto.DepartmentId);

            if (departmentExists == null)
            {
                throw new InvalidOperationException($"invaild Department with ID {nurse.DepartmentId} not found.");
            }

            // Update Nurse  properties from DTO
            nurse.FirstName = nurseDto.FirstName;
            nurse.LastName = nurseDto.LastName;
            nurse.Gender = nurseDto.Gender;
            nurse.ContactNumber = nurseDto.ContactNumber;
            nurse.shift = nurseDto.Shift;
            nurse.DepartmentId = nurseDto.DepartmentId;




            _context.Entry(nurse).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return nurseDto;

        }



        public async Task Delete(int nurseId)
        {
            var nurse = await _context.Nurses.FindAsync(nurseId);

            _context.Entry(nurse).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }






    }
}