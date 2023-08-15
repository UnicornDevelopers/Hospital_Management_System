
using System;
using Hospital_System.Data;
using Hospital_System.Models.DTOs;
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





        public async Task<NurseDTO> Create(NurseDTO nurseDTO)
        {
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
            // Find the Nurse by ID
            var nurse = await _context.Nurses.FindAsync(nurseID);

            if (nurse == null)
                return null;

            // Convert the Nurse to an NurseDTO.

            var nurseDTO = new NurseDTO
            {
                Id = nurse.Id,
                FirstName = nurse.FirstName,
                LastName = nurse.LastName,
                Gender = nurse.Gender,
                ContactNumber = nurse.ContactNumber,
                Shift = nurse.shift,
                DepartmentId = nurse.DepartmentId

            };

            return nurseDTO;


        }




        public async Task<List<NurseDTO>> GetNurses()
        {
            var nurses = await _context.Nurses.Include(nurse => nurse.department).ToListAsync();

            // Map Nurses entities to NurseDTOs
            var nursesDto = nurses.Select(n => new NurseDTO
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






        public async Task<NurseDTO> UpdateNurse(int id, NurseDTO nurseDto)
        {


            var nurse = await _context.Nurses.FindAsync(id);

            if (nurse == null)
                return null;


            // Update Nurse  properties from DTO
            nurse.FirstName = nurseDto.FirstName;
            nurse.LastName = nurseDto.LastName;
            nurse.Gender = nurseDto.Gender;
            nurse.ContactNumber = nurseDto.ContactNumber;
            nurse.shift = nurseDto.Shift;
            nurse.DepartmentId = nurseDto.DepartmentId;

            if (nurseDto.DepartmentId.HasValue)
            {
                nurse.department = await _context.Departments.FindAsync(nurseDto.DepartmentId);
            }
            else
            {
                nurse.department = null;
            }

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