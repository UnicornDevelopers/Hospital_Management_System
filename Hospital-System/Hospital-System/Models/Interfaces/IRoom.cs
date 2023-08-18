using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Room;

namespace Hospital_System.Models.Interfaces
{
    public interface IRoom
    {

        Task<OutRoomDTO> CreateRoom(OutRoomDTO room);

        // GET All
        Task<List<OutRoomDTO>> GetRooms();

        // GET Hotel By Id

        Task<RoomDTO> GetRoom(int RoomID);

        // Update
        Task<OutRoomDTO> UpdateRoom(int id, OutRoomDTO roomDto);

        // Delete 

        Task DeleteRoom(int id);

    }
}
