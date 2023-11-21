using HotelApplication.Exceptions;
using HotelApplication.Interfaces;
using HotelApplication.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelService _hotelService;
        private readonly ILogger _logger;

        public HotelController(IHotelService hotelService, ILogger<HotelController> logger)
        {
            _hotelService = hotelService;
            _logger = logger;
        }
        /// <summary>
        /// Adds a new hotel to the system.
        /// </summary>
        /// <param name="hotelDetails">The details of the hotel to be added.</param>
        /// <returns>The details of the newly added hotel.</returns>

        [HttpPost("AddHotel")]
        [Authorize(Roles = "Admin")]
        public ActionResult AddHotel(HotelDTO hotelDTO)
        {
            var hotel = _hotelService.AddHotel(hotelDTO);
            if (hotel != null)
            {
                _logger.LogInformation("Hotel Added");
                return Ok(hotel);
            }
            _logger.LogError("Unable to add the hotel");
            return BadRequest("Unable to add the hotel");
        }
        /// <summary>
        /// Retrieve hotel details based on the specified city.
        /// </summary>
        /// <param name="city">The city to use as a filter.</param>
        /// <returns>A display of hotels located in the given city.</returns>

        [HttpGet]
        public ActionResult GetHotel(int hotelId)
        {
            string message = string.Empty;
            try
            {
                var result = _hotelService.GetHotels(hotelId);
                _logger.LogInformation("Displayed Hotels");
                return Ok(result);

            }
            catch (NoHotelsAvailableException ex)
            {
                message = ex.Message;
            }
            _logger.LogError("Could not display hotels");
            return BadRequest(message);
        }
        /// <summary>
        /// Deletes a hotel based on the provided identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel to be deleted.</param>
        /// <returns>A message indicating the result of the deletion operation.</returns>

        [HttpDelete("RemoveHotel")]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveHotel(int id)
        {
            var result = _hotelService.RemoveHotel(id);
            if (result)
            {
                _logger.LogInformation("Hotel Removed");
                return Ok("Hotel removed successfully");

            }
            _logger.LogError("Could not remove hotel");
            return BadRequest("Could not remove hotel");

        }
        /// <summary>
        /// Updates the details of a hotel.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel.</param>
        /// <param name="hotelDTO">The information containing updated details of the hotel.</param>
        /// <returns>A message indicating the success of the update operation.</returns>

        [HttpPost("UpdateHotel")]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateHotel(int id, HotelDTO hotelDTO)
        {
            var result = _hotelService.UpdateHotel(id, hotelDTO);
            if (result != null)
            {
                _logger.LogInformation("Hotel Updated");
                return Ok(result);

            }
            _logger.LogError("Could not update hotel");
            return BadRequest("Could not update");

        }
    }
}