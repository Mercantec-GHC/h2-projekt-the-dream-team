using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;
using Microsoft.EntityFrameworkCore;

namespace H2Projekt.Infrastructure.Repositories
{
    public class BookingRepository : BaseRepository, IBookingRepository
    {
        public BookingRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<List<Booking>> GetAllBookingsAsync(CancellationToken cancellationToken)
        {
            return await _appDbContext.Bookings
                .Include(b => b.Guest)
                .Include(b => b.RoomType)
                .Include(b => b.Room)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Booking>> GetBookingsByGuestId(int guestId, CancellationToken cancellationToken)
        {
            return await _appDbContext.Bookings
                .Where(b => b.GuestId == guestId)
                .Include(b => b.Guest)
                .Include(b => b.RoomType)
                .Include(b => b.Room)
                .ToListAsync(cancellationToken);
        }

        public async Task<Booking?> GetBookingByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Bookings
                .Include(b => b.Guest)
                .Include(b => b.RoomType)
                .Include(b => b.Room)
                .SingleOrDefaultAsync(b => b.Id == id, cancellationToken);
        }
    }
}


