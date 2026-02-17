using FluentValidation;
using H2Projekt.Application.Commands.Bookings;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;
using H2Projekt.Domain.Enums;

namespace H2Projekt.Application.Handlers.Bookings
{
    public class UpdateBookingHandler
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IValidator<Booking> _validator;

        public UpdateBookingHandler(IBookingRepository bookingRepository, IGuestRepository guestRepository, IRoomRepository roomRepository, IValidator<Booking> validator)
        {
            _bookingRepository = bookingRepository;
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
            _validator = validator;
        }

        public async Task HandleAsync(UpdateBookingCommand request, CancellationToken cancellationToken = default)
        {
            var booking = await _bookingRepository.GetBookingByIdAsync(request.BookingId, cancellationToken);

            if (booking is null)
            {
                throw new NonExistentException($"Booking with id {request.BookingId} doesn't exist.");
            }

            var guest = await _guestRepository.GetGuestByIdAsync(booking.GuestId, cancellationToken);

            if (guest is null)
            {
                throw new NonExistentException($"Guest with id {booking.GuestId} doesn't exist.");
            }

            var room = await _roomRepository.GetRoomByIdAsync(request.RoomId, cancellationToken);

            if (room is null)
            {
                throw new NonExistentException($"Room with id {request.RoomId} doesn't exist.");
            }

            booking.AssignRoom(room);

            room.UpdateDetails(RoomAvailabilityStatus.Occupied);

            var validationResult = await _validator.ValidateAsync(booking, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _bookingRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
