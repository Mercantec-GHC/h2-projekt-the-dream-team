using FluentValidation;
using H2Projekt.Application.Commands.Guests;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Guests
{
    public class UpdateGuestHandler
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IValidator<Guest> _validator;

        public UpdateGuestHandler(IGuestRepository guestRepository, IValidator<Guest> validator)
        {
            _guestRepository = guestRepository;
            _validator = validator;
        }

        public async Task Handle(UpdateGuestCommand request, CancellationToken cancellationToken = default)
        {
            var existingGuest = await _guestRepository.GetGuestByIdAsync(request.Id, cancellationToken);

            if (existingGuest is null)
            {
                throw new NonExistentException($"Guest with id {request.Id} doesn't exist.");
            }

            existingGuest.UpdateDetails(request.FirstName, request.LastName, request.Email);

            var validationResult = await _validator.ValidateAsync(existingGuest);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.ToString("\n"));
            }

            await _guestRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
