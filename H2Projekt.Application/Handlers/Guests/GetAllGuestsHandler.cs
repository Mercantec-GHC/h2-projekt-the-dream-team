using H2Projekt.Application.Dto.Guests;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Guests
{
    public class GetAllGuestsHandler
    {
        private readonly IGuestRepository _guestRepository;

        public GetAllGuestsHandler(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<List<GuestDto>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var guests = await _guestRepository.GetAllGuestsAsync(cancellationToken);

            return guests.Select(guest => new GuestDto(guest)).ToList();
        }
    }
}
