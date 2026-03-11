using H2Projekt.Application.Commands.Authentication;
using H2Projekt.Application.Dto.Authentication;
using H2Projekt.Application.Helpers;
using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;
using System.Security.Authentication;

namespace H2Projekt.Application.Handlers.Authentication
{
    public class LoginHandler
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IJWTTokenService _tokenService;

        public LoginHandler(IGuestRepository guestRepository, IJWTTokenService tokenService)
        {
            _guestRepository = guestRepository;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto> HandleAsync(LoginCommand request, CancellationToken cancellationToken)
        {
            var guest = await _guestRepository.GetGuestByEmailAsync(request.Email, cancellationToken);

            if (guest is null)
            {
                throw new InvalidCredentialException("Invalid email or password.");
            }

            if (!PasswordHelper.VerifyPassword(request.Password, guest.PasswordHash))
            {
                throw new InvalidCredentialException("Invalid email or password.");
            }

            var refreshToken = _tokenService.GenerateRefreshToken();

            guest.SetRefreshToken(refreshToken);

            await _guestRepository.SaveChangesAsync(cancellationToken);

            return new AuthResponseDto()
            {
                AccessToken = _tokenService.GenerateAccessToken(guest),
                RefreshToken = refreshToken
            };
        }
    }
}
