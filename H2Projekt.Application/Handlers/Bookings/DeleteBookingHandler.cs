using H2Projekt.Application.Dto.Guests;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Bookings
{
    public class DeleteBookingHandler
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IGuestRepository _guestRepository;

        public DeleteBookingHandler(IBookingRepository bookingRepository, IGuestRepository guestRepository)
        {
            _bookingRepository = bookingRepository;
            _guestRepository = guestRepository;
        }

        public async Task HandleAsync(GuestDto workContext, int bookingId, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(bookingId, cancellationToken);

            if (booking is null)
            {
                throw new NonExistentException($"Booking with ID {bookingId} doesn't exist.");
            }

            if (!workContext.IsAdmin() && workContext.Id != booking.GuestId)
            {
                throw new ForbidException();
            }

            if (!booking.Guest.Bookings.Any(b => b.Id == bookingId))
            {
                throw new NonExistentException($"Booking with ID {bookingId} doesn't exist for the associated guest.");    
            }

            booking.Guest.RemoveBooking(booking);

            await _guestRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
