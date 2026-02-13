using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Guests
{
    public class DeleteGuestHandler
    {
        private readonly IGuestRepository _guestRepository;

        public DeleteGuestHandler(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            var guest = await _guestRepository.GetGuestByIdAsync(id, cancellationToken);

            if (guest is null)
            {
                throw new NonExistentException($"Guest with id {id} doesn't exist.");
            }

            await _guestRepository.DeleteGuestAsync(guest, cancellationToken);
        }
    }
}
