using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelApplication.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public string UserName { get; set; }
        [ForeignKey("UserName")]
        public User User { get; set; }
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; }
        public string DateOfBooking { get; set; }
        public int NoOfRoomsBooked { get; set; }
        public float Price { get; set; }
        //public ICollection<Room> Rooms { get; set; }

    }
}