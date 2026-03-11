using H2Projekt.Application.Commands.Guests;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Helpers;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Guests
{
    public class CreateGuestHandler
    {
        private readonly IGuestRepository _guestRepository;

        public CreateGuestHandler(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<int> HandleAsync(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            var guestExists = await _guestRepository.GuestExistsAsync(request.Email, cancellationToken);

            if (guestExists)
            {
                throw new DuplicateException("A guest with the same email already exists.");
            }

            var guest = new Guest(request.FirstName, request.LastName, request.Email, PasswordHelper.HashPassword("12345678"));

            await _guestRepository.AddGuestAsync(guest, cancellationToken);

            return guest.Id;
        }
    }
}
