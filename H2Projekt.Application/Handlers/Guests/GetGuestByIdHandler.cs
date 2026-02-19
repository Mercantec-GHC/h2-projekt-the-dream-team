using H2Projekt.Application.Dto.Guests;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Guests
{
    public class GetGuestByIdHandler
    {
        private readonly IGuestRepository _guestRepository;

        public GetGuestByIdHandler(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<GuestDto> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            var guest = await _guestRepository.GetGuestByIdAsync(id, cancellationToken);

            if (guest is null)
            {
                throw new NonExistentException($"Guest with id {id} doesn't exist.");
            }

            return new GuestDto(guest);
        }
    }
}
