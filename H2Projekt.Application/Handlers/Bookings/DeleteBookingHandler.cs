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

        public async Task HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id, cancellationToken);

            if (booking is null)
            {
                throw new NonExistentException($"Booking with id {id} doesn't exist.");
            }

            var guest = await _guestRepository.GetGuestByIdAsync(booking.GuestId, cancellationToken);

            if (guest is null)
            {
                throw new NonExistentException($"Guest with id {booking.GuestId} doesn't exist.");
            }

            guest.RemoveBooking(booking);

            await _guestRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
