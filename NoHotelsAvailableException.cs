namespace HotelApplication.Exceptions
{
    public class NoHotelsAvailableException : Exception
    {
        string message;
        public NoHotelsAvailableException()
        {
            message = "No hotels are available to stay";
        }
        public override string Message => message;
    }
}