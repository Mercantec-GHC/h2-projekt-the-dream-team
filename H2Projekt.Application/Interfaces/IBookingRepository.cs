using H2Projekt.Domain;

namespace H2Projekt.Application.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllBookingAsync(CancellationToken cancellationToken = default);
        Task<Booking?> GetBookingByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> CanCreateBookingAsync(RoomType roomType, DateOnly fromDate, DateOnly toDate, CancellationToken cancellationToken);
        Task<IReadOnlyList<Booking>> GetBookingsForRoomsAsync(IEnumerable<int> roomIds, DateOnly from, DateOnly to);
        Task<int> AddBookingAsync(Booking booking, CancellationToken cancellationToken);
        Task DeleteBookingAsync(Booking booking, CancellationToken cancellationToken = default);
    }
}
