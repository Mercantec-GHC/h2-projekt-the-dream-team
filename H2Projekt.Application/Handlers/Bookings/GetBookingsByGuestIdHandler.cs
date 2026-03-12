using H2Projekt.Application.Dto.Bookings;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Bookings
{
    public class GetBookingsByGuestIdHandler
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingsByGuestIdHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<BookingDto>> HandleAsync(int id, CancellationToken cancellationToken)
        {
            var bookings = await _bookingRepository.GetBookingsByGuestId(id, cancellationToken);

            return bookings.Select(booking => new BookingDto(booking)).ToList();
        }
    }
}
