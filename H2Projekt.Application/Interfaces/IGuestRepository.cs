using H2Projekt.Domain;

namespace H2Projekt.Application.Interfaces
{
    public interface IGuestRepository
    {
        Task<Guest?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> GuestExistsAsync(string email, CancellationToken cancellationToken = default);
        Task<int> AddGuestAsync(Guest guest, CancellationToken cancellationToken = default);
    }
}
