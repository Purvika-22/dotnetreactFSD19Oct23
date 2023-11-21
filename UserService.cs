using HotelApplication.Interfaces;
using HotelApplication.Models.DTOs;
using HotelApplication.Models;
using System.Security.Cryptography;
using System.Text;
using HotelApplication.Interfaces;

namespace HotelBookingApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<string, User> _repository;
        private readonly ITokenService _tokenService;

        public UserService(IRepository<string, User> repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }
        /// <summary>
        /// Authenticates a user by validating the provided credentials.
        /// </summary>
        /// <param name="loginCredentials">User login credentials</param>
        /// <returns>Returns a token upon successful authentication</returns>

        public UserDTO Login(UserDTO userDTO)
        {
            var user = _repository.GetById(userDTO.UserName);
            if (user != null)
            {
                HMACSHA512 hmac = new HMACSHA512(user.Key);
                var userpass = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                for (int i = 0; i < userpass.Length; i++)
                {
                    if (user.Password[i] != userpass[i])
                        return null;
                }
                userDTO.Role = user.Role;
                userDTO.Token = _tokenService.GetToken(userDTO);
                userDTO.Password = "";
                return userDTO;
            }
            return null;
        }
        /// <summary>
        /// Creates a new user account within the application.
        /// </summary>
        /// <param name="registrationData">Data containing user information for registration.</param>
        /// <returns>Returns an authentication token upon successful registration.</returns>

        public UserDTO Register(UserViewModel userviewmodel)
        {
            HMACSHA512 hmac = new HMACSHA512();
            User user = new User()
            {
                UserName = userviewmodel.UserName,
                Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(userviewmodel.Password)),
                ContactNumber = userviewmodel.ContactNumber,           
                Address = userviewmodel.Address,
                Key = hmac.Key,
                Role = userviewmodel.Role
            };
            var result = _repository.Add(user);
            if (result != null)
            {
                userviewmodel.Token = _tokenService.GetToken(userviewmodel);
                userviewmodel.Password = "";
                userviewmodel.ReTypePassword = "";
                return userviewmodel;
            }
            return null;

        }
    }
}