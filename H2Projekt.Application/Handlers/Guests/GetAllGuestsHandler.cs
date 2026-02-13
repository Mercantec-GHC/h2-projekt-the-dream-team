using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Guests
{
    public class GetAllGuestsHandler
    {
        private readonly IGuestRepository _guestRepository;

        public GetAllGuestsHandler(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<List<Guest>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var guests = await _guestRepository.GetAllGuestsAsync(cancellationToken);

            return guests;
        }
    }
}
