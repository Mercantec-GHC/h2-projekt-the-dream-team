using H2Projekt.Application.Exceptions;
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

        public async Task<Guest> HandleAsync(string email, CancellationToken cancellationToken = default)
        {
            var guest = await _guestRepository.GetGuestByEmailAsync(email, cancellationToken);
            if (guest is null)
                throw new NonExistentException($"Guest with Email {email} doesn't exist.");

            return guest;
        }
    }
}
