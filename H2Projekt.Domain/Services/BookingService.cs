/*using H2Projekt.Domain.Enums;

namespace H2Projekt.Domain.Service
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepo;
        private readonly IRoomRepository _roomRepo;
        private readonly IGuestRepository _guestRepo;

        public BookingService(IBookingRepository bookingRepo, IRoomRepository roomRepo, IGuestRepository guestRepo)
        {
            _bookingRepo = bookingRepo;
            _roomRepo = roomRepo;
            _guestRepo = guestRepo;
        }

        public async Task<Booking> CreateAsync(
            Guid guestId,
            RoomTypeEnum roomType,
            DateTimeOffset fromDate,
            DateTimeOffset toDate)
        {
            if (fromDate >= toDate)
                throw new ArgumentException("FromDate skal være før ToDate.");

            // 1) Verificér at gæsten findes (fordi Guest er en entitet)
            var guest = await _guestRepo.GetByIdAsync(guestId)
                       ?? throw new InvalidOperationException("Guest findes ikke.");

            // 2) Find kandidatrum af typen
            var candidates = await _roomRepo.GetByTypeAsync(roomType);
            if (candidates.Count == 0)
                throw new InvalidOperationException("Der findes ingen værelser af den ønskede type.");

            // 3) Hent eksisterende bookinger i perioden for disse rum
            var candidateIds = candidates.Select(r => r.Id).ToList();
            var existing = await _bookingRepo.GetBookingsForRoomsAsync(candidateIds, fromDate, toDate);

            // 4) Find første ledige rum uden overlap
            var assigned = candidates.FirstOrDefault(room =>
                !existing.Any(b =>
                    b.AssignedRoom != null &&
                    b.AssignedRoom.Id == room.Id &&
                    fromDate < b.ToDate && toDate > b.FromDate));

            if (assigned is null)
                throw new InvalidOperationException("Ingen ledige værelser i perioden.");

            // 5) Opret booking med FK til Guest
            var booking = new Booking(guestId, roomType, fromDate, toDate);
            booking.AssignRoom(assigned);

            // 6) Gem
            await _bookingRepo.AddAsync(booking);
            return booking;
        }
    }
}*/
