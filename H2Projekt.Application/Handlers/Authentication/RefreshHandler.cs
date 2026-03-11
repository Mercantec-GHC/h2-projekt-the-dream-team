using H2Projekt.Application.Dto.Authentication;
using H2Projekt.Application.Exceptions;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;

namespace H2Projekt.Application.Handlers.Authentication
{
    public class RefreshHandler
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IJWTTokenService _tokenService;

        public RefreshHandler(IGuestRepository guestRepository, IJWTTokenService tokenService)
        {
            _guestRepository = guestRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> HandleAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var guest = await _guestRepository.GetGuestByRefreshTokenAsync(refreshToken, cancellationToken);

            if (guest is null)
            {
                throw new NonExistentException("Invalid refresh token.");
            }

            var newRefreshToken = _tokenService.GenerateRefreshToken();

            guest.SetRefreshToken(newRefreshToken);

            await _guestRepository.SaveChangesAsync(cancellationToken);

            return new AuthResponseDto()
            {
                AccessToken = _tokenService.GenerateAccessToken(guest),
                RefreshToken = newRefreshToken
            };
        }
    }
}
