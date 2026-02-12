using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Bookings
{
    public class GetBookingByIdHandler
    {
        private readonly IBookingRepository _bookingRepository;

        public GetBookingByIdHandler(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<Booking> Handle(int id, CancellationToken cancellationToken = default)
        {
            var bookings = await _bookingRepository.GetBookingByIdAsync(id, cancellationToken);

            if (bookings is null)
            {
                throw new NonExistentException($"Booking with id {id} doesn't exist.");
            }

            return bookings;
        }
    }
}
