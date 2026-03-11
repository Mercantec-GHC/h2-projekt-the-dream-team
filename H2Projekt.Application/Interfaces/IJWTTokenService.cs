using H2Projekt.Domain;

namespace H2Projekt.Application.Interfaces
{
    public interface IJWTTokenService
    {
        string GenerateToken(Guest guest);
    }
}