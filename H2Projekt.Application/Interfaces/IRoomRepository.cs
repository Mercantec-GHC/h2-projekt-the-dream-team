using H2Projekt.Domain;

namespace H2Projekt.Application.Interfaces
{
    public interface IRoomRepository : IBaseRepository
    {
        #region Rooms

        Task<List<Room>> GetAllRoomsAsync(CancellationToken cancellationToken = default);
        Task<Room?> GetRoomByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Room?> GetRoomByNumberAsync(string number, CancellationToken cancellationToken = default);
        Task<bool> RoomExistsAsync(string number, CancellationToken cancellationToken = default);
        Task<int> AddRoomAsync(Room room, CancellationToken cancellationToken = default);
        Task DeleteRoomAsync(Room room, CancellationToken cancellationToken = default);

        #endregion

        #region Room Types

        Task<List<RoomType>> GetAllRoomTypesAsync(CancellationToken cancellationToken = default);
        Task<RoomType?> GetRoomTypeByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> RoomTypeExistsAsync(string name, CancellationToken cancellationToken = default);
        Task<int> AddRoomTypeAsync(RoomType roomType, CancellationToken cancellationToken = default);
        Task DeleteRoomTypeAsync(RoomType roomType, CancellationToken cancellationToken = default);

        #endregion
    }
}