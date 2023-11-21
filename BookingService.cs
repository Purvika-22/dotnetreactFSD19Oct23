using System.Net.Mail;
using System.Net;
using HotelApplication.Interfaces;
using HotelApplication.Models.DTOs;
using HotelApplication.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using HotelApplication.Exceptions;

namespace HotelApplication.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<int, Booking> _bookingRepository;
        private readonly IRepository<int, Room> _roomRepository;
        private readonly IRepository<int, Hotel> _hotelRepository;
        private readonly IRepository<string, User> _userRepository;

        public BookingService(IRepository<int, Booking> bookingRepository,
            IRepository<int, Room> roomRepository,
            IRepository<int, Hotel> hotelRepository,
            IRepository<string, User> userRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _hotelRepository = hotelRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Adds the booking details based on the provided bookingDTO
        /// </summary>
        /// <param name="bookingDTO">BookingDTO contains details of booking</param>
        /// <returns>returns the bookingDTo if the booking was succesfully added; otherwise returns a null value</returns>
        public BookingDTO AddBookingDetails(BookingDTO bookingDTO)
        {

            // Retrieve the room id based on the roomId from bookingDTO
            int roomId = bookingDTO.RoomId;
            var room = _roomRepository.GetById(roomId);
            var hotel = _hotelRepository.GetById(room.HotelId);

            //Calculate the amount for booking based on the price of the room and total number of room
            float amount = (bookingDTO.NoOfRoomsBooked * room.Price);
            DateTime dateTime = DateTime.Now;

            //Create a new booking object with the details from bookingDTO
            Booking booking = new Booking()
            {
                UserName = bookingDTO.UserName,
                RoomId = bookingDTO.RoomId,
                NoOfRoomsBooked = bookingDTO.NoOfRoomsBooked,
                DateOfBooking = dateTime.ToString(),
                Price = amount

            };
            //Add the new booking to the repository
            var result = _bookingRepository.Add(booking);
            var user = _userRepository.GetById(bookingDTO.UserName);
            string message = $"Dear {user.FullName},\nThank you for selecting {hotel.HotelName} for your upcoming stay!" +
     $" Your reservation has been successfully confirmed, and we can't wait to host you. " +
     $"Your unique booking reference number is {result.BookingId}." +
     $" \nWe're eager to ensure your stay is exceptional!\nWarm regards,\nThe {hotel.HotelName} Team\n{hotel.ContactNumber}";

            string subject = $"Reservation Confirmed - {hotel.HotelName}";

            string body = $"Dear {user.FullName},\nThank you for choosing {hotel.HotelName} for your stay! Your reservation is confirmed, and we are excited to welcome you." +
                $"\nReservation Details:-\nBooking Reference: {result.BookingId}\nTotal Amount: {amount}\n\nOur team is dedicated to making your time at {hotel.HotelName} unforgettable." +
                $" We wish you a pleasant journey!\nBest wishes,\nThe {hotel.HotelName} Team\n{hotel.ContactNumber}";


            //Check if the booking was added successfully and return the bookingDTO
            if (result != null)
            {
                SendBookingConfirmationEmail(bookingDTO.UserName, subject, body);
                // SendBookingConfirmationSms("+91"+user.Phone,message);
                return bookingDTO;
            }
            //Returns null if booking was not added successfully
            return null;
        }

        /// <summary>
        /// Retrieve a list of booking object based on the unique hotel identifier
        /// </summary>
        /// <param name="hotelId">The unique identifier of a hotel</param>
        /// <returns>Returns the list of booking object for the provided hotel; Otherwise return null</returns>
        public List<Booking> GetBooking(int hotelId)
        {
            //use LINQ to join booking and room entities based on room id and filtered by hotel id and project the result into new booking
            var bookings = (from Booking in _bookingRepository.GetAll()
                            join room in _roomRepository.GetAll() on Booking.RoomId equals room.RoomId
                            where room.HotelId == hotelId
                            select new Booking
                            {
                                BookingId = Booking.BookingId,
                                DateOfBooking = Booking.DateOfBooking,
                                RoomId = Booking.RoomId,
                                NoOfRoomsBooked = Booking.NoOfRoomsBooked,
                                Price = Booking.Price,
                                UserName = Booking.UserName
                            })
                    .ToList();

            //Check if the booking was found  and return the booking list; Otherwise return null
            if (bookings.Count > 0)
            {
                return bookings;
            }
            return null;
        }

        /// <summary>
        /// Retrieve the list of booking details based on the unique id of a user
        /// </summary>
        /// <param name="userId">Unique id of a user</param>
        /// <returns>Returns the list of booking object from the provided user id</returns>
        /// <exception cref="NoBookingsAvailableException">Thrown when no bookings are available for the specified user</exception>
        public List<Booking> GetUserBooking(string userEmail)
        {
            //Retrieve the booking details for the specified user
            var user = _bookingRepository.GetAll().Where(u => u.UserName == userEmail).ToList();

            //Check if the booking was found and return the booking list; Otherwise thows a NoBookingsAvailableException
            if (user != null)
            {
                return user;
            }
            throw new NoBookingsAvailableException();
        }


        /// <summary>
        /// Sends a confirmation email to the user with booking details.
        /// </summary>
        /// <param name="recipientEmail">The email address of the user.</param>
        /// <param name="emailSubject">Subject of the confirmation email.</param>
        /// <param name="emailBody">Body text of the confirmation email.</param>
        public void SendBookingConfirmationEmail(string recipientEmail, string subject, string body)
        {

            string email = "ghantapurvikaprasad@gmail.com";
            string password = "honey4275";

            // Recipient email
            string toEmail = recipientEmail;

            // Create the email message
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(email);
            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;

            // Set up SMTP client
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            smtpClient.Port = 587;
            smtpClient.Credentials = new NetworkCredential(email, password);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            // Send the email
            smtpClient.Send(mail);

        }
    }
       
}