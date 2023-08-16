using Hospital_System.Models.DTOs.Room;

namespace Hospital_System.Models.Interfaces
{
    public interface IRoom
    {

        Task<RoomDTO> CreateRoom(RoomDTO room);

        // GET All
        Task<List<RoomDTO>> GetRooms();

        // GET Hotel By Id

        Task<RoomDTO> GetRoom(int RoomID);

        // Update
        Task<RoomDTO> UpdateRoom(int id, RoomDTO roomDto);

        // Delete 

        Task DeleteRoom(int id);

    }
}
