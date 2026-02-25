using H2Projekt.Application.Commands.Bookings;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Bookings
{
    public class CreateBookingHandler
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;

        public CreateBookingHandler(IBookingRepository bookingRepository, IGuestRepository guestRepository, IRoomRepository roomRepository)
        {
            _guestRepository = guestRepository;
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }

        public async Task<int> HandleAsync(CreateBookingCommand request, CancellationToken cancellationToken = default)
        {
            var roomType = await _roomRepository.GetRoomTypeByIdAsync(request.RoomTypeId, cancellationToken);

            if (roomType is null)
            {
                throw new NonExistentException($"Room type with id {request.RoomTypeId} doesn't exist.");
            }

            var rooms = await _roomRepository.GetAllRoomsByRoomTypeAsync(roomType.Id, cancellationToken);

            var bookings = await _bookingRepository.GetAllBookingsForRoomTypeAsync(roomType.Id, cancellationToken);

            if (!rooms.Any() || bookings.Count(booking => booking.FromDate <= request.ToDate && booking.ToDate >= request.FromDate) >= rooms.Count)
            {
                throw new NonExistentException("There are no available rooms of the selected type for the given dates.");
            }

            var guest = await _guestRepository.GetGuestByIdAsync(request.GuestId, cancellationToken);

            if (guest is null)
            {
                throw new NonExistentException($"Guest with id {request.GuestId} doesn't exist.");
            }

            var discount = await _roomRepository.GetRoomDiscountForPeriodAsync(roomType.Id, request.FromDate, request.ToDate, cancellationToken);

            var booking = new Booking(guest.Id, roomType.Id, request.FromDate, request.ToDate, request.NumberOfAdults, request.NumberOfChildren, request.TravelingWithPets, discount?.PricePerNight ?? roomType.PricePerNight);

            guest.AddBooking(booking);

            await _guestRepository.SaveChangesAsync(cancellationToken);

            return booking.Id;
        }

    }
}
