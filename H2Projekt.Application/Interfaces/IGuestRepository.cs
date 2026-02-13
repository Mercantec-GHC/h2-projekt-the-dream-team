using H2Projekt.Domain;

namespace H2Projekt.Application.Interfaces
{
    public interface IGuestRepository : IBaseRepository
    {
        Task<List<Guest>> GetAllGuestsAsync(CancellationToken cancellationToken = default);
        Task<Guest?> GetGuestByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<Guest?> GetGuestByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<bool> GuestExistsAsync(string email, CancellationToken cancellationToken = default);
        Task<int> AddGuestAsync(Guest guest, CancellationToken cancellationToken = default);
        Task DeleteGuestAsync(Guest guest, CancellationToken cancellationToken = default);
    }
}
