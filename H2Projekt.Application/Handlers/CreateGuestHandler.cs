using FluentValidation;
using H2Projekt.Application.Commands;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace H2Projekt.Application.Handlers
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
        public async Task<int> HandleAsync(CreateGuestCommand request, CancellationToken cancellationToken)
        {
            var exists = await _repository.GuestExistsAsync(request.Email);
            if (exists)
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
