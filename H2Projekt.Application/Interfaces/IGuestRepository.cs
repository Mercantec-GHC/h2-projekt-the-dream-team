using H2Projekt.Domain;

namespace H2Projekt.Application.Interfaces
{
    public interface IGuestRepository : IBaseRepository
    {
        Task<List<Guest>> GetAllGuestsAsync(CancellationToken cancellationToken);
        Task<Guest?> GetGuestByIdAsync(int id, CancellationToken cancellationToken);
        Task<Guest?> GetGuestByEmailAsync(string email, CancellationToken cancellationToken);
        Task<bool> GuestExistsAsync(string email, CancellationToken cancellationToken);
        Task<int> AddGuestAsync(Guest guest, CancellationToken cancellationToken);
        Task DeleteGuestAsync(Guest guest, CancellationToken cancellationToken);
    }
}
