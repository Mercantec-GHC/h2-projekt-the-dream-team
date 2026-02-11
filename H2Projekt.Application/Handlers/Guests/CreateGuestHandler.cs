using FluentValidation;
using H2Projekt.Application.Commands.Guests;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Guests
{
    public class CreateGuestHandler
    {
        private readonly IGuestRepository _repository;
        private readonly IValidator<Guest> _validator;

        public CreateGuestHandler(IGuestRepository repository, IValidator<Guest> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> HandleAsync(CreateGuestCommand request, CancellationToken cancellationToken = default)
        {
            var guestExists = await _repository.GuestExistsAsync(request.Email, cancellationToken);

            if (guestExists)
            {
                throw new DuplicateException("A guest with the same email already exists.");
            }

            var guest = new Guest(request.FirstName, request.LastName, request.Email);

            var validationResult = await _validator.ValidateAsync(guest, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _repository.AddGuestAsync(guest, cancellationToken);

            return guest.Id;
        }
    }
}
