using H2Projekt.Domain;

namespace H2Projekt.Application.Interfaces
{
    public interface IBookingRepository : IBaseRepository
    {
        Task<List<Booking>> GetAllBookingsAsync(CancellationToken cancellationToken = default);
        Task<Booking?> GetBookingByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> IsRoomTypeAvailableAsync(RoomType roomType, DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken);
        Task<IReadOnlyList<Booking>> GetBookingsForRoomsAsync(IEnumerable<int> roomIds, DateOnly from, DateOnly to);
    }
}
