using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;
using Microsoft.EntityFrameworkCore;

namespace H2Projekt.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _appDbContext;

        public RoomRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Room>> GetAllRoomsAsync(CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Rooms.ToListAsync(cancellationToken);
        }

        public async Task<Room?> GetRoomByRoomNumberAsync(string number, CancellationToken cancellationToken)
        {
            return await _appDbContext.Rooms.SingleOrDefaultAsync(r => r.Number == number, cancellationToken);
        }

        public async Task<int> AddAsync(Room room, CancellationToken cancellationToken = default)
        {
            await _appDbContext.Rooms.AddAsync(room);

            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync(Room room, CancellationToken cancellationToken = default)
        {
            _appDbContext.Rooms.Remove(room);

            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
