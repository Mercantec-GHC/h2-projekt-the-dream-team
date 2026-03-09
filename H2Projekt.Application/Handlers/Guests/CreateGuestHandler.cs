using H2Projekt.Application.Commands.Guests;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Guests
{
    public class CreateGuestHandler
    {
        private readonly IGuestRepository _repository;

        public CreateGuestHandler(IGuestRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> HandleAsync(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            var guestExists = await _repository.GuestExistsAsync(request.Email, cancellationToken);

            if (guestExists)
            {
                throw new DuplicateException("A guest with the same email already exists.");
            }

            var guest = new Guest(request.FirstName, request.LastName, request.Email);

            await _repository.AddGuestAsync(guest, cancellationToken);

            return guest.Id;
        }
    }
}
