namespace H2Projekt.Application.Exceptions
{
    public class RoomNonExistentException : Exception
    {
        public RoomNonExistentException(string message) : base(message)
        {
        }
    }
}
