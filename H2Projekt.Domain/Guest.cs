using FluentValidation;
using H2Projekt.Domain.Enums;
using H2Projekt.Domain.Validators.Guests;

namespace H2Projekt.Domain
{
    public class Guest : EntityBase
    {
        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        public string PasswordHash { get; private set; } = default!;

        private readonly List<Booking> bookings = new List<Booking>();
        public IReadOnlyCollection<Booking> Bookings => bookings.AsReadOnly();

        public Guest() { }

        public Guest(string firstName, string lastName, string email, string passwordHash)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;

            ThrowIfInvalid();
        }

        public void UpdateDetails(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;

            ThrowIfInvalid();
        }

        public void AddBooking(Booking booking)
        {
            bookings.Add(booking);

            ThrowIfInvalid();
        }

        public void RemoveBooking(Booking booking)
        {
            bookings.Remove(booking);

            ThrowIfInvalid();
        }

        private void ThrowIfInvalid()
        {
            var validator = new GuestValidator();

            var result = validator.Validate(this);

            if (result.IsValid)
            {
                return;
            }

            throw new ValidationException(result.Errors);
        }
    }
}
