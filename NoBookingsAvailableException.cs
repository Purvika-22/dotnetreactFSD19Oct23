namespace HotelApplication.Exceptions
{
    public class NoBookingsAvailableException : Exception
    {
        string message;
        public NoBookingsAvailableException()
        {
            message = "No bookings are available ";
        }
        public override string Message => message;
    }
}