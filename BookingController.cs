using HotelApplication.Controllers;
using HotelApplication.Interfaces;
using HotelApplication.Models.DTOs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<UserController> _logger;

        public BookingController(IBookingService bookingService, ILogger<UserController> logger)
        {
            _bookingService = bookingService;
            _logger = logger;
        }
        /// <summary>
        /// Adds booking information to the database.
        /// </summary>
        /// <param name="bookingDetails">The details of the booking to be added.</param>
        /// <returns>The details of the newly added booking.</returns>

        [HttpPost("AddBooking")]
        [Authorize(Roles = "User")]
        public ActionResult AddBooking(BookingDTO bookingDTO)
        {
            var booking = _bookingService.AddBookingDetails(bookingDTO);
            if (booking != null)
            {
                _logger.LogInformation("Booked successfully");
                return Ok(booking);
            }
            _logger.LogError("Unable to book the rooms");
            return BadRequest("Unable to book the rooms");
        }
        /// <summary>
        /// Retrieves the booking details associated with a specific hotel.
        /// </summary>
        /// <param name="hotelId">The unique identifier of the hotel to retrieve booking details for.</param>
        /// <returns>A collection of booking information for the specified hotel.</returns>

        [HttpGet("AdminBooking")]
        [Authorize(Roles = "Admin")]
        public ActionResult GetAdminBooking(int id)
        {
            var booking = _bookingService.GetBooking(id);
            if (booking != null)
            {
                _logger.LogInformation("Admin booking details displayed");
                return Ok(booking);
            }
            _logger.LogError("Unable to display admin bookings");
            return BadRequest("No bookings found");
        }
        /// <summary>
        /// Retrieves booking details for a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A collection of booking details associated with the user.</returns>

        [HttpGet("UserBooking")]
        [Authorize(Roles = "User")]
        public ActionResult GetUserBooking(string id)
        {
            var booking = _bookingService.GetUserBooking(id);
            if (booking != null)
            {
                _logger.LogInformation("User booking details displayed");
                return Ok(booking);
            }
            _logger.LogError("Could not display user bookings");
            return BadRequest("No bookings found");
        }

    }
}