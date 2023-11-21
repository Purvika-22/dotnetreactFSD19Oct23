using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelApplication.Models.DTOs
{
    public class BookingDTO
    {
        [Required(ErrorMessage = "Username cannot be empty")]
        public string UserName { get; set; }  

        [Required(ErrorMessage = "Room ID cannot be empty")]
        public int RoomId { get; set; }
        public int NoOfRoomsBooked { get; set; }
        public string DateOfBooking { get; set; }
    }
}