using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Guests
{
    public class GetGuestByEmailHandler
    {
        private readonly IGuestRepository _guestRepository;

        public GetGuestByEmailHandler(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public Task<Guest?> HandleAsync(string email, CancellationToken ct = default)
        {
            return _guestRepository.GetGuestByEmailAsync(email.Trim().ToLower(), ct);
        }
    }
}
