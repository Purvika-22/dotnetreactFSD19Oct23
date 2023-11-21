using System.ComponentModel.DataAnnotations;

namespace HotelApplication.Models.DTOs
{
    public class HotelDTO
    {
       
        [Required(ErrorMessage = "Hotel name cannot be empty")]
        public string HotelName { get; set; }

        [Required(ErrorMessage = "UserId cannot be empty")]

        public string UserName { get; set; }
        [Required(ErrorMessage = "City cannot be empty")]
        public string Location { get; set; }
    
        [Required(ErrorMessage = "ContactNumber cannot be empty")]
      
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Description cannot be empty")]
       
        public string Description { get; set; }
    }
}