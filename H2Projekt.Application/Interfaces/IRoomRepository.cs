using H2Projekt.Domain;

namespace H2Projekt.Application.Interfaces
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllRoomsAsync(CancellationToken cancellationToken = default);
        Task<Room?> GetRoomByRoomNumberAsync(string number, CancellationToken cancellationToken);
        Task<int> AddAsync(Room room, CancellationToken cancellationToken = default);
        Task UpdateAsync(Room room, CancellationToken cancellationToken = default);
        Task DeleteAsync(Room room, CancellationToken cancellationToken = default);
    }
}