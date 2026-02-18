using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;
using Microsoft.EntityFrameworkCore;

namespace H2Projekt.Infrastructure.Repositories
{
    public class RoomRepository : BaseRepository, IRoomRepository
    {
        public RoomRepository(AppDbContext appDbContext) : base(appDbContext) { }

        #region Rooms

        public async Task<List<Room>> GetAllRoomsAsync(CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.Bookings)
                    .ThenInclude(b => b.RoomType)
                .OrderBy(r => r.Id).ToListAsync(cancellationToken);
        }

        public async Task<Room?> GetRoomByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.Bookings)
                    .ThenInclude(b => b.RoomType)
                .SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<Room?> GetRoomByNumberAsync(string number, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.Rooms
                .Include(r => r.RoomType)
                .Include(r => r.Bookings)
                    .ThenInclude(b => b.RoomType)
                .SingleOrDefaultAsync(r => r.Number == number, cancellationToken);
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

        public async Task DeleteRoomTypeAsync(RoomType roomType, CancellationToken cancellationToken = default)
        {
            _appDbContext.RoomTypes.Remove(roomType);

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region Room Discounts

        public async Task<List<RoomDiscount>> GetAllRoomDiscountsAsync(CancellationToken cancellationToken = default)
        {
            return await _appDbContext.RoomDiscounts.Include(rd => rd.RoomType).OrderBy(rd => rd.Id).ToListAsync(cancellationToken);
        }

        public async Task<RoomDiscount?> GetRoomDiscountByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.RoomDiscounts.Include(rd => rd.RoomType).SingleOrDefaultAsync(rd => rd.Id == id, cancellationToken);
        }

        public async Task<RoomDiscount?> GetRoomDiscountForPeriodAsync(int roomTypeId, DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken = default)
        {
            return await _appDbContext.RoomDiscounts.Include(rd => rd.RoomType).SingleOrDefaultAsync(rd => rd.RoomTypeId == roomTypeId && rd.FromDate <= fromDate && rd.ToDate >= toDate, cancellationToken);
        }

        public async Task<int> AddRoomDiscountAsync(RoomDiscount roomDiscount, CancellationToken cancellationToken = default)
        {
            await _appDbContext.RoomDiscounts.AddAsync(roomDiscount);

            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteRoomDiscountAsync(RoomDiscount roomDiscount, CancellationToken cancellationToken = default)
        {
            _appDbContext.RoomDiscounts.Remove(roomDiscount);

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}
