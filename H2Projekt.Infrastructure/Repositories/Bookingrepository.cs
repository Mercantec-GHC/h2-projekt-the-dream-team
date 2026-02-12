using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;
using Microsoft.EntityFrameworkCore;
namespace H2Projekt.Infrastructure.Repositories
{
    public class BookingRepository : BaseRepository, IBookingRepository
    {

        public BookingRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<List<Booking>> GetAllBookingAsync(CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Bookings.ToListAsync(cancellationToken);
        }

        public async Task<Booking?> GetBookingByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Bookings.SingleOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<bool> CanCreateBookingAsync(RoomType roomType, DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken)
        {
            var rooms = await _appDbContext.Rooms.CountAsync(room => room.RoomTypeId == roomType.Id);

            var bookings = await _appDbContext.Bookings.CountAsync(booking => booking.RoomType.Id == roomType.Id && booking.FromDate == fromDate && booking.ToDate == toDate);

            return bookings < rooms;
        }

        public async Task<int> AddBookingAsync(Booking booking, CancellationToken cancellationToken = default)
        {
            await _appDbContext.Bookings.AddAsync(booking);

            return await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(Booking booking, CancellationToken cancellationToken = default)
        {
            _appDbContext.Bookings.Remove(booking);

            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Booking>> GetBookingsForRoomsAsync(IEnumerable<int> roomIds, DateOnly from, DateOnly to)
        {
            return await _appDbContext.Bookings
                .Where(b => b.AssignedRoom != null
                         && roomIds.Contains(b.AssignedRoom.Id)
                         && from < b.ToDate
                         && to > b.FromDate)
                .ToListAsync();
        }
    }
}


