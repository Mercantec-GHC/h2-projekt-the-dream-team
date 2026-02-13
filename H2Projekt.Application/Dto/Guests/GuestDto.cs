using H2Projekt.Application.Dto.Bookings;
using H2Projekt.Domain;

namespace H2Projekt.Application.Dto.Guests
{
    public class GuestDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public List<BookingWithoutGuestDto> Bookings { get; set; } = new List<BookingWithoutGuestDto>();

        public GuestDto(Guest guest)
        {
            Id = guest.Id;
            FirstName = guest.FirstName;
            LastName = guest.LastName;
            Email = guest.Email;
            Bookings = guest.Bookings.Select(b => new BookingWithoutGuestDto(b)).ToList();
        }
    }
}
