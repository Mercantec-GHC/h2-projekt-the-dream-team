using H2Projekt.Domain;

namespace H2Projekt.Application.Dto.Guests
{
    public class GuestWithoutBookingsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;

        public GuestWithoutBookingsDto(Guest guest)
        {
            Id = guest.Id;
            FirstName = guest.FirstName;
            LastName = guest.LastName;
            Email = guest.Email;
        }
    }
}
