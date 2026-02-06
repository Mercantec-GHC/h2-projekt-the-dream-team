namespace H2Projekt.Application.Exceptions
{
    public class RoomDuplicateException : Exception
    {
        public RoomDuplicateException(string message) : base(message)
        {
        }
    }
}
