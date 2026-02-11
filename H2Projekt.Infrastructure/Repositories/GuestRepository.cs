using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;
using Microsoft.EntityFrameworkCore;

namespace H2Projekt.Infrastructure.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private readonly AppDbContext _appDbContext;

        public GuestRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Guest?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Guests.SingleOrDefaultAsync(g => g.Id == id, cancellationToken);
        }

        public async Task<bool> GuestExistsAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Guests.AnyAsync(g => g.Email == email, cancellationToken);
        }

        public async Task<int> AddGuestAsync(Guest guest, CancellationToken cancellationToken = default)
        {
            await _appDbContext.Guests.AddAsync(guest, cancellationToken);

            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
