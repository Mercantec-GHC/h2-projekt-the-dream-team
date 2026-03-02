using H2Projekt.Application.Commands.Bookings;
using H2Projekt.Application.Dto.Bookings;
using H2Projekt.Application.Dto.Guests;
using H2Projekt.Application.Dto.Rooms;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Bookings
{
    public class GetBookingsOverviewHandler
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public GetBookingsOverviewHandler(IBookingRepository bookingRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }

        public async Task<ICollection<BookingDto>> HandleAsync()
        {
            

            var bookings = await _bookingRepository.GetAllBookingsAsync();
            var rooms = await _roomRepository.GetAllRoomsAsync();

            var unassigned = bookings.Where(b => b.RoomId == null).OrderBy(b => b.FromDate);

            foreach (var booking in unassigned)
            {
                var candidateRooms = rooms.Where(r => r.RoomTypeId == booking.RoomTypeId);

                foreach (var room in candidateRooms)
                {
                    bool overlap = bookings.Any(b => b.RoomId == room.Id && 
                        ((booking.FromDate >= b.FromDate && booking.FromDate < b.ToDate) ||
                         (booking.ToDate > b.FromDate && booking.ToDate <= b.ToDate) ||
                         (booking.FromDate <= b.FromDate && booking.ToDate >= b.ToDate)));

                    if (!overlap)
                    {
                        booking.AssignRoom(room);
                        break;
                    }
                }
            }
            return bookings.Select(b => new BookingDto(b)).ToList();
        }
    }
}
