using HotelApplication.Interfaces;
using HotelApplication.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotelApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        /// <summary>
        /// Creates a new user account with the provided user registration information.
        /// </summary>
        /// <param name="registrationInfo">Details of the user registration</param>
        /// <returns>Returns the user details after successful registration</returns>

        [HttpPost("register")]
        public ActionResult Register(UserViewModel userviewmodel)
        {
            string message = "";
            try
            {
                var user = _userService.Register(userviewmodel);
                if (user != null)
                {
                    _logger.LogInformation("User Registerd");
                    return Ok(user);
                }
            }
            catch (DbUpdateException exp)
            {
                message = "Duplicate username";
            }
            catch (Exception)
            {
                _logger.LogError("Could not register user");
            }
            return BadRequest(message);
        }
        /// <summary>
        /// Authenticates the user.
        /// </summary>
        /// <param name="loginCredentials">User login details.</param>
        /// <returns>A response message indicating the outcome 
        /// of the authentication attempt.</returns>

        [HttpPost("login")]
        public ActionResult Login(UserDTO userDTO)
        {
            var result = _userService.Login(userDTO);
            if (result != null)
            {
                _logger.LogInformation("Login Successful");
                return Ok(result);
            }
            _logger.LogError("Login failed");
            return Unauthorized("Invalid username or password");
        }
    }
}