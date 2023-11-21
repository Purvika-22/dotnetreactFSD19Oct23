using System.ComponentModel.DataAnnotations;

namespace HotelApplication.Models
{
    public class User
    {
        public string FullName { get; set; }
        [Key]
        public string UserName { get; set; }

        public byte[] Password { get; set; }


        public string ContactNumber { get; set; }

        public string Address { get; set; }

        public string Role { get; set; }
        public byte[] Key { get; set; }

        public ICollection<Room> Rooms { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}