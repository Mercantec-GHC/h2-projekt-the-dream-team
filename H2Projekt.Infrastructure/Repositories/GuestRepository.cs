using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;
using Microsoft.EntityFrameworkCore;

namespace H2Projekt.Infrastructure.Repositories
{
    public class GuestRepository : BaseRepository, IGuestRepository
    {
        public GuestRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<List<Guest>> GetAllGuestsAsync(CancellationToken cancellationToken)
        {
            return await _appDbContext.Guests
                .Include(g => g.Bookings)
                    .ThenInclude(b => b.RoomType)
                .Include(g => g.Bookings)
                    .ThenInclude(b => b.Room)
                .ToListAsync(cancellationToken);
        }

        public async Task<Guest?> GetGuestByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _appDbContext.Guests
                .Include(g => g.Bookings)
                    .ThenInclude(b => b.RoomType)
                .Include(g => g.Bookings)
                    .ThenInclude(b => b.Room)
                .SingleOrDefaultAsync(g => g.Id == id, cancellationToken);
        }

        public async Task<Guest?> GetGuestByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _appDbContext.Guests
                .Include(g => g.Bookings)
                    .ThenInclude(b => b.RoomType)
                .Include(g => g.Bookings)
                    .ThenInclude(b => b.Room)
                .SingleOrDefaultAsync(g => g.Email == email, cancellationToken);
        }

        public async Task<Guest?> GetGuestByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            return await _appDbContext.Guests
                .Include(g => g.Bookings)
                    .ThenInclude(b => b.RoomType)
                .Include(g => g.Bookings)
                    .ThenInclude(b => b.Room)
                .SingleOrDefaultAsync(g => g.RefreshToken == refreshToken, cancellationToken);
        }

        public async Task<bool> GuestExistsAsync(string email, CancellationToken cancellationToken)
        {
            return await _appDbContext.Guests.AnyAsync(g => g.Email == email, cancellationToken);
        }

        public async Task<int> AddGuestAsync(Guest guest, CancellationToken cancellationToken)
        {
            await _appDbContext.Guests.AddAsync(guest, cancellationToken);

            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteGuestAsync(Guest guest, CancellationToken cancellationToken)
        {
            _appDbContext.Guests.Remove(guest);

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
