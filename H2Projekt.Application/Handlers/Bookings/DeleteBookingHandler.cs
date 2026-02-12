using H2Projekt.Application.Commands.Bookings;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace H2Projekt.Application.Handlers.Bookings
{
    public class DeleteBookingHandler
    {
        private readonly IGuestRepository _guestRepository;

        public DeleteBookingHandler(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task Handle(DeleteBookingCommand request, CancellationToken cancellationToken = default)
        {
            var guest = await _guestRepository.GetGuestByIdAsync(request.GuestId, cancellationToken);

            if (guest is null)
            {
                throw new NonExistentException($"Guest with id {request.GuestId} doesn't exist.");
            }

            if (!guest.Bookings.Any(b => b.Id == request.BookingId))
            {
                throw new NonExistentException($"Booking with id {request.BookingId} doesn't exist for guest with id {request.GuestId}.");
            }

            var booking = guest.Bookings.First(b => b.Id == request.BookingId);

            guest.RemoveBooking(booking);

            await _guestRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
