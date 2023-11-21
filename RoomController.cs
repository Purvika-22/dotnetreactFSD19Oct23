using HotelApplication.Interfaces;
using HotelApplication.Models.DTOs;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly ILogger _logger;

        public RoomController(IRoomService roomService, ILogger<RoomController> logger)
        {
            _roomService = roomService;
            _logger = logger;
        }
        /// <summary>
        /// Adds a new room to the hotel.
        /// </summary>
        /// <param name="roomDTO">The information of the room to be added.</param>
        /// <returns>The details of the newly added room.</returns>

        [HttpPost("CreateRooms")]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateRooms(RoomDTO roomDTO)
        {
            var room = _roomService.AddRoom(roomDTO);
            if (room != null)
            {
                _logger.LogInformation("Room Created");
                return Ok(room);

            }
            _logger.LogError("Unable to add room");
            return BadRequest("Could not add rooms");

        }
        /// <summary>
        /// Removes a room from the hotel.
        /// </summary>
        /// <param name="roomId">The ID of the room to be removed.</param>
        /// <returns>A message indicating the success of the deletion.</returns>

        [HttpDelete("DeleteRooms")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteRooms(int id)
        {
            bool roomId = _roomService.RemoveRoom(id);
            if (roomId)
            {
                _logger.LogInformation("Room Deleted");
                return Ok("The room has been deleted successfully");

            }
            _logger.LogError("Unable to delete room");
            return BadRequest("Invalid roomId");
        }
        /// <summary>
        /// Updates the details of a room.
        /// </summary>
        /// <param name="id">The identifier of the room to be updated.</param>
        /// <param name="roomDTO">The data transfer object containing the updated details of the room.</param>
        /// <returns>A message indicating the successful update of the room.</returns>


        [HttpPost("UpdateRooms")]
        [Authorize(Roles = "Admin")]
        public ActionResult UpdateRooms(int id, RoomDTO roomDTO)
        {
            var room = _roomService.UpdateRoom(id, roomDTO);
            if (room != null)
            {
                _logger.LogInformation("Room Updated");
                return Ok("Room updated successfully");

            }
            _logger.LogError("Unable to update room");
            return BadRequest("Unable to update");

        }
    }
}