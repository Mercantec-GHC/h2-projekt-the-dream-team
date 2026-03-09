using H2Projekt.Domain;

namespace H2Projekt.Application.Interfaces
{
    public interface IRoomRepository : IBaseRepository
    {
        #region Rooms

        Task<List<Room>> GetAllRoomsAsync(CancellationToken cancellationToken);
        Task<Room?> GetRoomByIdAsync(int id, CancellationToken cancellationToken);
        Task<Room?> GetRoomByNumberAsync(string number, CancellationToken cancellationToken);

        #endregion

        #region Room Types

        Task<List<RoomType>> GetAllRoomTypesAsync(CancellationToken cancellationToken);
        Task<RoomType?> GetRoomTypeByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> RoomTypeExistsAsync(string name, CancellationToken cancellationToken);
        Task<int> AddRoomTypeAsync(RoomType roomType, CancellationToken cancellationToken);
        Task DeleteRoomTypeAsync(RoomType roomType, CancellationToken cancellationToken);

        #endregion

        #region Room Discounts

        Task<List<RoomDiscount>> GetAllRoomDiscountsAsync(CancellationToken cancellationToken);
        Task<RoomDiscount?> GetRoomDiscountByIdAsync(int id, CancellationToken cancellationToken);
        Task<RoomDiscount?> GetRoomDiscountForPeriodAsync(int roomTypeId, DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken);

        #endregion
    }
}