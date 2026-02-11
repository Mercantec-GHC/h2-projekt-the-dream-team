
namespace H2Projekt.Domain
{
    public class Guest : EntityBase
    {
        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;
        public string Email { get; private set; } = default!;

        private readonly List<Booking> bookings = new List<Booking>();
        public IReadOnlyCollection<Booking> Bookings => bookings.AsReadOnly();

        public Guest() { }

        public Guest(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void UpdateDetails(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void AddBooking(Booking booking)
        {
            bookings.Add(booking);
        }

        public void RemoveBooking(Booking booking)
        {
            bookings.Remove(booking);
        }
    }
}
