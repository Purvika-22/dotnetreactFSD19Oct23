using HotelApplication.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelApplication.Models
{
    public class Hotel
    {

        [Key]
        public int HotelId { get; set; }


        public string UserName { get; set; }
        [ForeignKey("UserName")]
        public User User { get; set; }

        public string HotelName { get; set; }


        public string Location { get; set; }

        public string ContactNumber { get; set; }


        public string? Description { get; set; }

        public ICollection<Room> Rooms { get; set; }


    }
}