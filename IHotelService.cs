using HotelApplication.Models;
using HotelApplication.Models.DTOs;

namespace HotelApplication.Interfaces
{
    public interface IHotelService
    {
        List<Hotel> GetHotels(int hotelId);
        HotelDTO AddHotel(HotelDTO hotelDTO);
        HotelDTO UpdateHotel(int id, HotelDTO hotelDTO);
        bool RemoveHotel(int id);
    }
}