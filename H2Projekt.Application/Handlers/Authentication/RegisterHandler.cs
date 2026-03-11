using H2Projekt.Application.Commands.Authentication;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Helpers;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Authentication
{
    public class RegisterHandler
    {
        private readonly IGuestRepository _guestRepository;

        public RegisterHandler(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<int> HandleAsync(RegisterCommand request, CancellationToken cancellationToken)
        {
            var guestExists = await _guestRepository.GuestExistsAsync(request.Email, cancellationToken);

            if (guestExists)
            {
                throw new DuplicateException("A guest with the same email already exists.");
            }

            var guest = new Guest(request.FirstName, request.LastName, request.Email, PasswordHelper.HashPassword(request.Password));

            await _guestRepository.AddGuestAsync(guest, cancellationToken);

            return guest.Id;
        }
    }
}
