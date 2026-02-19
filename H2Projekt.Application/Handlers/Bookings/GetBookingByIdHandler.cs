using H2Projekt.Application.Dto.Bookings;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;

namespace H2Projekt.Application.Handlers.Bookings
{
    public class GetBookingByIdHandler
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingByIdHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<BookingDto> HandleAsync(int id, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(id, cancellationToken);

            if (booking is null)
            {
                throw new NonExistentException($"Booking with id {id} doesn't exist.");
            }

            return new BookingDto(booking);
        }
    }
}
