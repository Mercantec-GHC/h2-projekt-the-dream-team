using FluentValidation;
using H2Projekt.Application.Commands.Bookings;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace H2Projekt.Application.Handlers.Bookings
{
    public class CreateBookingHandler
    {
        private readonly IBookingRepository _bookingrepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IValidator<Guest> _validator;

        public CreateBookingHandler(IBookingRepository bookingrepository, IGuestRepository guestrepository, IValidator<Guest> validator)
        {
            _guestRepository = guestrepository;
            _bookingrepository = bookingrepository;
            _validator = validator;
        }

        public async Task<int> HandleAsync(CreateBookingCommand request, CancellationToken cancellationToken = default)
        {
            var canBook = await _bookingrepository.CanCreateBookingAsync(request.RoomType, request.FromDate, request.ToDate, cancellationToken);

            if (!canBook)
            {
                throw new Exception("A booking already exists for the specified room and date range.");
            }

            var guest = await _guestRepository.GetGuestByIdAsync(request.GuestId, cancellationToken);

            if (guest is null)
            {
                throw new NonExistentException($"Guest with id{request.GuestId} doesn't exist ");
            }

            var booking = new Booking(request.GuestId, request.RoomType.Id, request.FromDate, request.ToDate, request.RoomType.PricePerNight);

            guest.AddBooking(booking);

            var validationResult = await _validator.ValidateAsync(guest, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            await _guestRepository.SaveChangesAsync(cancellationToken);

            return booking.Id;
        }

    }
}
