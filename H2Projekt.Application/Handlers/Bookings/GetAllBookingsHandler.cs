using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Bookings
{
    public class GetAllBookingsHandler
    {
        private readonly IBookingRepository _bookingRepository;

        public GetAllBookingsHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<Booking>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync(cancellationToken);

            return bookings;
        }
    }
}