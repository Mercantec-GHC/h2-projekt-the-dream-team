using FluentValidation;
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
        private readonly IValidator<Guest> _validator;

        public CreateBookingHandler(IBookingRepository bookingRepository, IGuestRepository guestRepository, IRoomRepository roomRepository, IValidator<Guest> validator)
        {
            _guestRepository = guestRepository;
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
            _validator = validator;
        }

        public async Task<int> HandleAsync(CreateBookingCommand request, CancellationToken cancellationToken = default)
        {
            var roomType = await _roomRepository.GetRoomTypeByIdAsync(request.RoomTypeId, cancellationToken);

            if (roomType is null)
            {
                throw new NonExistentException($"Room type with id {request.RoomTypeId} doesn't exist.");
            }

            var isRoomTypeAvailable = await _bookingRepository.IsRoomTypeAvailableAsync(roomType, request.FromDate, request.ToDate, cancellationToken);

            if (!isRoomTypeAvailable)
            {
                throw new NonExistentException("There are no available rooms of the selected type for the given dates.");
            }

            var guest = await _guestRepository.GetGuestByIdAsync(request.GuestId, cancellationToken);

            if (guest is null)
            {
                throw new NonExistentException($"Guest with id {request.GuestId} doesn't exist.");
            }

            var discount = await _roomRepository.GetRoomDiscountForPeriodAsync(roomType.Id, request.FromDate, request.ToDate, cancellationToken);

            var pricePerNight = discount is not null ? discount.PricePerNight : roomType.PricePerNight;

            var booking = new Booking(guest.Id, roomType.Id, request.FromDate, request.ToDate, pricePerNight);

            guest.AddBooking(booking);

            var validationResult = await _validator.ValidateAsync(guest, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _guestRepository.SaveChangesAsync(cancellationToken);

            return booking.Id;
        }

    }
}
