using System.ComponentModel.DataAnnotations;

namespace HotelApplication.Models.DTOs
{
    public class RoomDTO
    {     
        [Required(ErrorMessage = "Hotel ID cannot be empty")]
        public int HotelId { get; set; }
   
        [Required(ErrorMessage = "Price cannot be empty")]
        public float Price { get; set; }
   
        [Required(ErrorMessage = "Description cannot be empty")]
        public string Description { get; set; }
  
        [Required(ErrorMessage = "Amenities cannot be empty")]
        public List<string> RoomAmenities { get; set; }

    }
}