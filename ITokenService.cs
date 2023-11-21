using HotelApplication.Models.DTOs;

namespace HotelApplication.Interfaces
{
    public interface ITokenService
    {
        string GetToken(UserDTO user);
    }
}