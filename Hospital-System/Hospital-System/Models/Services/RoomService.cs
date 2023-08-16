using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
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
    // CREATE Room........................................................................
    public async Task<RoomDTO> CreateRoom(RoomDTO newRoomDTO)
    {
        Room room = new Room
        {
            Id = newRoomDTO.Id,
            RoomNumber = newRoomDTO.RoomNumber,
            RoomAvailability = newRoomDTO.RoomAvailability,
            NumberOfBeds = newRoomDTO.NumberOfBeds,
        };
        _context.Entry(room).State = EntityState.Added;
        await _context.SaveChangesAsync();
        return newRoomDTO;
    }
    // Get Room........................................................................
    public async Task<List<RoomDTO>> GetRooms()
    {
        var room = await _context.Rooms.Select(x => new RoomDTO()
        {
            Id = x.Id,
            RoomNumber = x.RoomNumber,
            RoomAvailability = x.RoomAvailability,
            NumberOfBeds = x.NumberOfBeds,
            Patients = x.Patients.Select(x => new PatientDTO()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                DoB = x.DoB,
                Gender = x.Gender,
                ContactNumber = x.ContactNumber,
                Address = x.Address,
            }).ToList()
        }).ToListAsync();
        return room;
    }
    // Get Room by ID........................................................................
    public async Task<RoomDTO> GetRoom(int id)
    {
            var room = await _context.Rooms.Select(x => new RoomDTO()
            {
                Id = x.Id,
                RoomNumber = x.RoomNumber,
                RoomAvailability = x.RoomAvailability,
                NumberOfBeds = x.NumberOfBeds,
                Patients = x.Patients.Select(x => new PatientDTO()
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
    // Update Room by ID........................................................................
    public async Task<RoomDTO> UpdateRoom(int id, RoomDTO updateRoomDTO)
    {
        Room room = new Room
        {
            Id = updateRoomDTO.Id,
            RoomNumber = updateRoomDTO.RoomNumber,
            RoomAvailability = updateRoomDTO.RoomAvailability,
            NumberOfBeds = updateRoomDTO.NumberOfBeds,
        };
        _context.Entry(room).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return updateRoomDTO;
    }
    // Delete Room by ID........................................................................
    public async Task DeleteRoom(int id)
    {
            Room room = await _context.Rooms.FindAsync(id);
        _context.Entry(room).State = EntityState.Deleted;
        await _context.SaveChangesAsync();
    }
}
}

