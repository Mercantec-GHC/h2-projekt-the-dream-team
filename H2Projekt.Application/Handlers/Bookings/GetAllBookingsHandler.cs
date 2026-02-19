using H2Projekt.Application.Dto.Bookings;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Bookings
{
    public class GetAllBookingsHandler
    {
        private readonly IBookingRepository _bookingRepository;

        public GetAllBookingsHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<BookingDto>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync(cancellationToken);

            return bookings.Select(booking => new BookingDto(booking)).ToList();
        }
    }
}