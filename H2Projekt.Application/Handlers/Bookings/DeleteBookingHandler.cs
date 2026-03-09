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

        public async Task HandleAsync(int id, CancellationToken cancellationToken)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id, cancellationToken);

            if (booking is null)
            {
                throw new NonExistentException($"Booking with ID {id} doesn't exist.");
            }

            booking.Guest.RemoveBooking(booking);

            await _guestRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
