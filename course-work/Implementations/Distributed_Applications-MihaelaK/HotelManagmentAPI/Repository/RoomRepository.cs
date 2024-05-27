using HotelManagmentAPI.Data;
using HotelManagmentAPI.Interfaces;
using System.Collections.Generic;
using System.Linq;

public class RoomRepository : IHotelRepository
{
    private readonly DataContext _context;

    public RoomRepository(DataContext context)
    {
        _context = context;
    }

    public ICollection<Room> GetRooms()
    {
        return _context.Rooms.OrderBy(r => r.RoomID).ToList();
    }

    public Room GetRoom(int roomID)
    {
        return _context.Rooms.FirstOrDefault(r => r.RoomID == roomID);
    }

    public Room GetRoomByType(string roomType)
    {
        return _context.Rooms.FirstOrDefault(r => r.RoomType == roomType);
    }

    public bool RoomExists(int roomID)
    {
        return _context.Rooms.Any(r => r.RoomID == roomID);
    }

    public bool CreateRoom(Room room)
    {
        _context.Rooms.Add(room);
        return Save();
    }

    public bool Save()
    {
       var saved = _context.SaveChanges();
        return saved >= 0 ? true : false;
    }
}
