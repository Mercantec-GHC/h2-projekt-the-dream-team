using H2Projekt.Domain;

namespace H2Projekt.Application.Interfaces
{
    public interface IBookingRepository : IBaseRepository
    {
        Task<List<Booking>> GetAllBookingsAsync(CancellationToken cancellationToken = default);
        Task<List<Booking>> GetAllBookingsForRoomTypeAsync(int roomTypeId, CancellationToken cancellationToken = default);
        Task<Booking?> GetBookingByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
