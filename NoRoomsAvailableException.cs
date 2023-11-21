namespace HotelApplication.Exceptions
{
    public class NoRoomsAvailableException : Exception
    {
        string message;
        public NoRoomsAvailableException()
        {
            message = "No rooms are available to stay";
        }
        public override string Message => message;
    }
}