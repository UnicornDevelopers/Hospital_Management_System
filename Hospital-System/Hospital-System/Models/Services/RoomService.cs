using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Patient;
using Hospital_System.Models.DTOs.Room;
using Hospital_System.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;


namespace Hospital_System.Models.Services
{
    public class RoomService : IRoom
    {
        private readonly HospitalDbContext _context;
        public RoomService(HospitalDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Creates a new room.
        /// </summary>
        /// <param name="room">The room data to be created.</param>
        /// <returns>The created room's information.</returns>
        public async Task<OutRoomDTO> CreateRoom(OutRoomDTO newRoomDTO)
        {
            var department = await _context.Departments.FindAsync(newRoomDTO.DepartmentId);
            if (department == null)
            {
                throw new ArgumentException("Invalid DepartmentId");
            }

            Room room = new Room
            {
                Id = newRoomDTO.Id,
                RoomNumber = newRoomDTO.RoomNumber,
                RoomAvailability = newRoomDTO.RoomAvailability,
                NumberOfBeds = newRoomDTO.NumberOfBeds,
                DepartmentId = newRoomDTO.DepartmentId,
            };

            _context.Entry(room).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return newRoomDTO;
        }

        /// <summary>
        /// Retrieves a list of all rooms.
        /// </summary>
        /// <returns>A list of all rooms.</returns>
        public async Task<List<OutRoomDTO>> GetRooms()
        {
            var rooms = await _context.Rooms
                .Include(r => r.Patients)
                .Select(x => new OutRoomDTO()
                {
                    Id = x.Id,
                    RoomNumber = x.RoomNumber,
                    RoomAvailability = x.RoomAvailability,
                    NumberOfBeds = x.NumberOfBeds,
                    DepartmentId = x.DepartmentId,
                }).ToListAsync();

            return rooms;
        }

        /// <summary>
        /// Retrieves detailed information about a specific room.
        /// </summary>
        /// <param name="RoomID">The ID of the room to retrieve.</param>
        /// <returns>Detailed information about the requested room.</returns>
        public async Task<RoomDTO> GetRoom(int id)
        {
            var room = await _context.Rooms.Select(x => new RoomDTO()
            {
                Id = x.Id,
                RoomNumber = x.RoomNumber,
                RoomAvailability = x.RoomAvailability,
                NumberOfBeds = x.NumberOfBeds,
                Patients = x.Patients.Select(x => new OutPatientDTO()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    DoB = x.DoB,
                    Gender = x.Gender,
                    ContactNumber = x.ContactNumber,
                    Address = x.Address,
                }).ToList()
            }).FirstOrDefaultAsync(x => x.Id == id);
            return room;
        }
        /// <summary>
        /// Updates the details of an existing room.
        /// </summary>
        /// <param name="id">The ID of the room to update.</param>
        /// <param name="roomDto">The updated room data.</param>
        /// <returns>The updated room's information.</returns>
        public async Task<OutRoomDTO> UpdateRoom(int id, OutRoomDTO updateRoomDTO)
        {
            var existingRoom = await _context.Rooms.FindAsync(id);

            if (existingRoom == null)
            {
                throw new ArgumentException($"Room with ID {id} not found.");
            }

            var existingDepartment = await _context.Departments.FindAsync(updateRoomDTO.DepartmentId);
            if (existingDepartment == null)
            {
                throw new ArgumentException($"Department with ID {updateRoomDTO.DepartmentId} not found.");
            }

            // Update the room properties
            existingRoom.RoomNumber = updateRoomDTO.RoomNumber;
            existingRoom.RoomAvailability = updateRoomDTO.RoomAvailability;
            existingRoom.NumberOfBeds = updateRoomDTO.NumberOfBeds;
            existingRoom.DepartmentId = updateRoomDTO.DepartmentId;

            _context.Entry(existingRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return updateRoomDTO;
        }


        /// <summary>
        /// Deletes a room by its ID.
        /// </summary>
        /// <param name="id">The ID of the room to delete.</param>

        public async Task DeleteRoom(int id)
        {
            Room room = await _context.Rooms.FindAsync(id);
            _context.Entry(room).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}

