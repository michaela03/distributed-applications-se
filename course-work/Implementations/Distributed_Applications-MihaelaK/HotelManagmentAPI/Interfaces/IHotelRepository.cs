namespace HotelManagmentAPI.Interfaces
{
    public interface IHotelRepository
    {
        ICollection<Room> GetRooms();
        Room GetRoom(int roomID);
        Room GetRoomByType(string roomType); 
        bool RoomExists(int roomID);

        bool CreateRoom(Room room);
        bool Save();
    }
}
