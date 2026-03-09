using FluentValidation;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain.Enums;

namespace H2Projekt.Application.Handlers.Bookings
{
    public class AssignRoomToBookingHandler
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;

        public AssignRoomToBookingHandler(IBookingRepository bookingRepository, IGuestRepository guestRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
        }

        public async Task<int> HandleAsync(int id, CancellationToken cancellationToken)
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync(cancellationToken);

            if (!bookings.Any(booking => booking.Id == id))
            {
                throw new NonExistentException($"Booking with id {id} doesn't exist.");
            }

            var booking = bookings.First(booking => booking.Id == id);

            var guest = await _guestRepository.GetGuestByIdAsync(booking.GuestId, cancellationToken);

            if (guest is null)
            {
                throw new NonExistentException($"Guest with id {booking.GuestId} doesn't exist.");
            }

            var rooms = await _roomRepository.GetAllRoomsAsync(cancellationToken);

            var filteredRooms = rooms.Where(room => room.RoomType.Id == booking.RoomTypeId && !bookings.Any(b =>
                (b.Room is not null && b.Room.Id == room.Id)
                && b.Id != booking.Id
                && b.FromDate <= booking.ToDate
                && b.ToDate >= booking.FromDate
            ));

            var room = filteredRooms.OrderBy(_ => Guid.NewGuid()).FirstOrDefault();

            if (room is null)
            {
                throw new NonExistentException($"No available rooms for the specified room type and dates.");
            }

            booking.AssignRoom(room);

            room.UpdateStatus(RoomAvailabilityStatus.Occupied);

            await _bookingRepository.SaveChangesAsync(cancellationToken);

            return room.Id;
        }
    }
}
