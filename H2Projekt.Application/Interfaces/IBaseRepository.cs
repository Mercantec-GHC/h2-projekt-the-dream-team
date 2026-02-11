namespace H2Projekt.Application.Interfaces
{
    public interface IBaseRepository
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}