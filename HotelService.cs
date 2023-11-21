using HotelApplication.Interfaces;
using HotelApplication.Exceptions;
using HotelApplication.Interfaces;
using HotelApplication.Models;
using HotelApplication.Models.DTOs;
using HotelApplication.Repositories;

namespace HotelApplication.Services
{
    public class HotelService : IHotelService
    {
        private readonly IRepository<int, Hotel> _hotelRepository;
        private readonly IRepository<int, Room> _roomRepository;

        public HotelService(IRepository<int, Hotel> repository, IRepository<int, Room> roomRepository)
        {
            _hotelRepository = repository;
            _roomRepository = roomRepository;
        }
        /// <summary>
        /// Adds a new hotel to the database based on the 
        /// provided hotel details in the hotelDTO.
        /// </summary>
        /// <param name="hotelDTO">The hotelDTO object containing 
        /// the details of the hotel to be added.</param>
        /// <returns>Returns the HotelDTO representing the added hotel 
        /// if the operation was successful; otherwise, returns null.</returns>

        public HotelDTO AddHotel(HotelDTO hotelDTO)
        {
           
            Hotel hotel = new Hotel()
            {
                HotelName = hotelDTO.HotelName,
                Location = hotelDTO.Location,
                UserName = hotelDTO.UserName,
                ContactNumber = hotelDTO.ContactNumber,
                Description = hotelDTO.Description,
            };
            
            var result = _hotelRepository.Add(hotel);
            
            if (result != null)
            {
                return hotelDTO;
            }
           
            return null;
        }
        
        public List<Hotel> GetHotels(int hotelId)
        {
            var hotels = _hotelRepository.GetAll().Where(h => h.HotelId == hotelId).ToList();

            if (hotels.Count == 0)
            {
                throw new NoHotelsAvailableException();
            }

            return hotels;
        }


        /// <summary>
        /// Deletes a hotel based on its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel to be deleted.</param>
        /// <returns>
        /// Returns true if the hotel was removed successfully; otherwise, returns false.
        /// </returns>

        public bool RemoveHotel(int id)
        {

            var result = _hotelRepository.Delete(id);
            if (result != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Updates a hotel using its unique identifier and the provided hotel data.
        /// </summary>
        /// <param name="id">The unique identifier of the hotel to be updated.</param>
        /// <param name="hotelDTO">An object containing the updated details of the hotel.</param>
        /// <returns>Returns the updated hotel data (as a hotelDTO) upon a successful update; otherwise, returns null.</returns>

        public HotelDTO UpdateHotel(int id, HotelDTO hotelDTO)
        {            
            var hotel = _hotelRepository.GetById(id);

            if (hotel != null)
            {
                //Update the hotel details provided by the hotelDTO
                hotel.ContactNumber = hotelDTO.ContactNumber;
                hotel.HotelName = hotelDTO.HotelName;
                hotel.Location = hotelDTO.Location;
                hotel.Description = hotelDTO.Description;
                var result = _hotelRepository.Update(hotel);
                return hotelDTO;
            }
            return null;
        }
    }
}