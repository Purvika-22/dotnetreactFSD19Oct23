using HotelApplication.Exceptions;
using HotelApplication.Interfaces;
using HotelApplication.Interfaces;
using HotelApplication.Models;
using HotelApplication.Models.DTOs;
using HotelApplication.Repositories;
using System.Runtime.Serialization;

namespace HotelApplication.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRepository<int, Room> _roomrepository;
        private readonly IRepository<int, RoomAmenity> _roomAmenityRepository;
        private readonly IRepository<int, Booking> _bookingRepository;

        public RoomService(IRepository<int, Room> repository, IRepository<int, RoomAmenity> roomAmenityRepository, IRepository<int, Booking> bookingRepository)
        {
            _roomrepository = repository;
            _roomAmenityRepository = roomAmenityRepository;
            _bookingRepository = bookingRepository;
        }
        /// <summary>
        /// Adds a room based on the provided roomDTO.
        /// </summary>
        /// <param name="roomDTO">The roomDTO containing room details.</param>
        /// <returns>Returns the roomDTO if the room is added successfully; otherwise, returns null.</returns>

        public RoomDTO AddRoom(RoomDTO roomDTO)
        {
            //Create a new room object with details provided by the roomDTO
            Room room = new Room()
            {
             
                Price = roomDTO.Price,
                HotelId = roomDTO.HotelId,
                Description = roomDTO.Description,
            };

            //Add the room to the repository
            var result = _roomrepository.Add(room);

            //Retrieve the roomId of the added room
            int id = room.RoomId;

            //Iterator through each room amenities and add to the repository
            foreach (string a in roomDTO.RoomAmenities)
            {
                RoomAmenity roomAmenity = new RoomAmenity()
                {
                    RoomId = id,
                    Amenities = a,
                };
                _roomAmenityRepository.Add(roomAmenity);
            }


            //Check if the room is added sucessfully return roomDTO; Otherwise return null
            if (result != null)
            {
                return roomDTO;
            }
            return null;
        }


        public List<Room> GetRooms(int hotelId)
        {
            var rooms = _roomrepository.GetAll().Where(r => r.HotelId == hotelId).ToList();

            if (rooms.Count == 0)
            {
                throw new NoRoomsAvailableException();
            }

            return rooms;
        }





        /// <summary>
        /// Removes a room from the system based on the provided identifier.
        /// </summary>
        /// <param name="roomId">The unique identifier of the room to be deleted.</param>
        /// <returns>Returns true if the room was successfully deleted; otherwise, returns false.</returns>

        public bool RemoveRoom(int id)
        {
            
            var roomcheck = _roomrepository.Delete(id);
            if (roomcheck != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Update a room using the specified ID and room details.
        /// </summary>
        /// <param name="id">The unique identifier of the room to be updated.</param>
        /// <param name="updatedRoomDetails">The DTO (Data Transfer Object)
        /// containing the updated details for the room.</param>
        /// <returns>Returns the updated room DTO if the operation is successful;
        /// otherwise, returns null.</returns>

        public RoomDTO UpdateRoom(int id, RoomDTO roomDTO)
        {
           
            var room = _roomrepository.GetById(id);

            
            if (room != null)
            {
             
                room.Price = roomDTO.Price;             
                room.Description = roomDTO.Description;              
                var result = _roomrepository.Update(room);
            
                return roomDTO;
            }
            
            return null;
        }
    }
}