using H2Projekt.Domain;
using H2Projekt.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace H2Projekt.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _appDbContext;

        public BookingRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task AddBookingAsync(Booking booking)
        {
            throw new NotImplementedException();
        }

        public async Task AddBookingSync(Booking booking)
        {
            await _appDbContext.Bookings.AddAsync(booking);
            await _appDbContext.SaveChangesAsync();
        }

        public Task<IReadOnlyList<Booking>> GetBookingForRoomAsync(IEnumerable<Guid> roomIds, DateTimeOffset from, DateTimeOffset to)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Booking>> GetBookingsForRoomsAsync(IEnumerable<int> roomIds, DateTimeOffset from, DateTimeOffset to)
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


