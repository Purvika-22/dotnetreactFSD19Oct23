using HotelApplication.Models.DTOs;
using HotelApplication.Models;

namespace HotelApplication.Interfaces
{
    public interface IBookingService
    {
        public BookingDTO AddBookingDetails(BookingDTO bookingDTO);
        public List<Booking> GetUserBooking(string userEmail);

        public List<Booking> GetBooking(int hotelId);
    }
}