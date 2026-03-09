using H2Projekt.Application.Dto.Bookings;
using H2Projekt.Application.Dto.Rooms;
using H2Projekt.Application.Interfaces;

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

        public async Task<BookingOverviewDto> HandleAsync(CancellationToken cancellationToken)
        {
            // Get all bookings 
            var bookings = await _bookingRepository.GetAllBookingsAsync(cancellationToken);

            // Get all rooms 
            var rooms = await _roomRepository.GetAllRoomsAsync(cancellationToken);

            // Create a dictionary to track bookings without assigned rooms and their possible room assignments
            var bookingsWithNoRoom = new Dictionary<int, int>();

            // Loop through bookings without assigned rooms and try to find possible room assignments
            foreach (var booking in bookings.Where(b => b.Room is null).OrderBy(b => b.FromDate))
            {
                // If this booking has already been processed and assigned a possible room, skip it
                if (bookingsWithNoRoom.ContainsKey(booking.Id))
                {
                    continue;
                }

                // Find candidate rooms that match the booking's room type
                var candidateRooms = rooms.Where(r => r.RoomType.Id == booking.RoomType.Id).ToList();

                // Loop through candidate rooms and check for overlapping bookings
                foreach (var room in candidateRooms)
                {
                    // Check if there are any overlapping bookings for this room
                    bool overlaps = bookings.Any(b =>
                        (b.Room?.Id == room.Id || (b.Room is null && bookingsWithNoRoom.TryGetValue(b.Id, out var rid) && rid == room.Id)) &&
                        b.Id != booking.Id &&
                        b.FromDate <= booking.ToDate &&
                        b.ToDate >= booking.FromDate
                    );

                    // If there are no overlaps, assign this room as a possible room for the booking
                    if (!overlaps)
                    {
                        // Assign the room as a possible room for the booking
                        bookingsWithNoRoom[booking.Id] = room.Id;

                        // Break out of the loop since we only need one possible room assignment per booking
                        break;
                    }
                }
            }

            // Map to BookingOverviewBookingDto
            var bookingOverviews = bookings.Select(booking =>
            {
                // If the booking has no assigned room, check if we found a possible room for it
                var possibleRoom = booking.Room is null ? rooms.FirstOrDefault(room => room.Id == bookingsWithNoRoom.GetValueOrDefault(booking.Id)) : null;

                // Return the BookingOverviewBookingDto with the possible room assignment if applicable
                return new BookingOverviewBookingDto(booking)
                {
                    PossibleRoom = possibleRoom is not null ? new RoomDto(possibleRoom) : null,
                };
            }).ToList();

            // Return the list of BookingOverviewDto
            return new BookingOverviewDto()
            {
                Dates = bookings.SelectMany(b => new[] { b.FromDate, b.ToDate }).Distinct().Order().ToList(),
                RoomTypes = bookings.Select(b => b.RoomType).Distinct().Select(rt => new RoomTypeDto(rt)).OrderBy(rt => rt.Id).ToList(),
                Bookings = bookingOverviews.OrderBy(b => b.FromDate).ToList()
            };
        }
    }
}
