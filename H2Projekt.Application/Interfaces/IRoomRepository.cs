using H2Projekt.Domain;

namespace H2Projekt.Application.Interfaces
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAllRoomsAsync(CancellationToken cancellationToken = default);
        Task<int> AddAsync(Room room, CancellationToken cancellationToken = default);
    }
}