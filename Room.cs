using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelApplication.Models
{
    public class Room
    {

        [Key]
        public int RoomId { get; set; }

        public int HotelId { get; set; }
        [ForeignKey("HotelId")]
        public Hotel Hotel { get; set; }

        public float Price { get; set; }


        public string? Description { get; set; }

        public string UserName { get; set; }
        public User User { get; set; }
        public Booking Booking { get; set; }
    }
}