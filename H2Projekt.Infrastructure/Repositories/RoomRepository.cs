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

        #region Rooms

        public async Task<List<Room>> GetAllRoomsAsync(CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Rooms.Include(x => x.RoomType).OrderBy(r => r.Id).ToListAsync(cancellationToken);
        }

        public async Task<Room?> GetRoomByNumberAsync(string number, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Rooms.SingleOrDefaultAsync(r => r.Number == number, cancellationToken);
        }

        public async Task<bool> RoomExistsAsync(string number, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Rooms.AnyAsync(r => r.Number == number, cancellationToken);
        }

        public async Task<int> AddRoomAsync(Room roomType, CancellationToken cancellationToken = default)
        {
            await _appDbContext.Rooms.AddAsync(roomType);

            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRoomAsync(Room roomType, CancellationToken cancellationToken = default)
        {
            _appDbContext.Rooms.Update(roomType);

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteRoomAsync(Room room, CancellationToken cancellationToken = default)
        {
            _appDbContext.Rooms.Remove(room);

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region Room Types

        public async Task<List<RoomType>> GetAllRoomTypesAsync(CancellationToken cancellationToken = default)
        {
            return await _appDbContext.RoomTypes.OrderBy(rt => rt.Id).ToListAsync(cancellationToken);
        }

        public async Task<RoomType?> GetRoomTypeByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.RoomTypes.SingleOrDefaultAsync(rt => rt.Id == id, cancellationToken);
        }

        public async Task<bool> RoomTypeExistsAsync(string name, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.RoomTypes.AnyAsync(rt => rt.Name == name, cancellationToken);
        }

        public async Task<int> AddRoomTypeAsync(RoomType roomType, CancellationToken cancellationToken = default)
        {
            await _appDbContext.RoomTypes.AddAsync(roomType);

            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRoomTypeAsync(RoomType roomType, CancellationToken cancellationToken = default)
        {
            _appDbContext.RoomTypes.Update(roomType);

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteRoomTypeAsync(RoomType roomType, CancellationToken cancellationToken = default)
        {
            _appDbContext.RoomTypes.Remove(roomType);

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        #endregion 
    }
}
