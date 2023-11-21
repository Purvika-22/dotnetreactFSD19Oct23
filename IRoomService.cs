using HotelApplication.Models;
using HotelApplication.Models.DTOs;

namespace HotelApplication.Interfaces
{
    public interface IRoomService
    {
        List<Room> GetRooms(int hotelid);
        RoomDTO AddRoom(RoomDTO room);
        RoomDTO UpdateRoom(int id, RoomDTO room);
        bool RemoveRoom(int id);
    }
}