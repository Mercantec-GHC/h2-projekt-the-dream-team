namespace H2Projekt.Application.Exceptions
{
    public class NonExistentException : Exception
    {
        public NonExistentException(string message) : base(message)
        {
        }
    }
}
