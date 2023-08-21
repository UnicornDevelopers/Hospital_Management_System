using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Room;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hospital_System.Models.Interfaces
{
    public interface IRoom
    {
        // Create a new room
        Task<OutRoomDTO> CreateRoom(OutRoomDTO room);

        // Get all rooms
        Task<List<OutRoomDTO>> GetRooms();

        // Get a room by its ID
        Task<RoomDTO> GetRoom(int RoomID);

        // Update a room
        Task<OutRoomDTO> UpdateRoom(int id, OutRoomDTO roomDto);

        // Delete a room
        Task DeleteRoom(int id);
    }
}
