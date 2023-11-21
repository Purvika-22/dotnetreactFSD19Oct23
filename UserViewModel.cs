using System.ComponentModel.DataAnnotations;

namespace HotelApplication.Models.DTOs
{
    public class UserViewModel : UserDTO
    {      
        [Required(ErrorMessage = "Re type password cannot be empty")]
        [Compare("Password", ErrorMessage = "Password and retype password do not match")]
        public string ReTypePassword { get; set; }     
      
        [Required(ErrorMessage = "Phone cannot be empty")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "Address cannot be empty")]
        public string Address { get; set; }

        public string FullName { get; set; }

    }
}