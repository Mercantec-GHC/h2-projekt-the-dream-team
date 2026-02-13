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
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IValidator<Guest> _validator;

        public UpdateBookingHandler(IGuestRepository guestRepository, IRoomRepository roomRepository, IValidator<Guest> validator)
        {
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
            _validator = validator;
        }

        public async Task HandleAsync(UpdateBookingCommand request, CancellationToken cancellationToken = default)
        {
            var guest = await _guestRepository.GetGuestByIdAsync(request.GuestId, cancellationToken);

            if (guest is null)
            {
                throw new NonExistentException($"Guest with id {request.GuestId} doesn't exist.");
            }

            if (!guest.Bookings.Any(b => b.Id == request.BookingId))
            {
                throw new NonExistentException($"Booking with id {request.BookingId} doesn't exist for guest with id {request.GuestId}.");
            }

            var booking = guest.Bookings.First(b => b.Id == request.BookingId);

            var room = await _roomRepository.GetRoomByIdAsync(request.RoomId, cancellationToken);

            if (room is null)
            {
                throw new NonExistentException($"Room with id {request.RoomId} doesn't exist.");
            }

            booking.AssignRoom(room);

            room.UpdateDetails(RoomAvailabilityStatus.Occupied);

            var validationResult = await _validator.ValidateAsync(guest, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await _guestRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
