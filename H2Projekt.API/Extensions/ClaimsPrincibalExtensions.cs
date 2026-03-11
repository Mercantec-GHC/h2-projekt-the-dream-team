using H2Projekt.Application.Dto.Guests;
using System.Security.Claims;

namespace H2Projekt.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static GuestDto? GetWorkContextOrNull(this ClaimsPrincipal principal)
        {
            if (principal?.Identity is null || !principal.Identity.IsAuthenticated)
            {
                return null;
            }

            var idClaim = principal.Claims.FirstOrDefault(claim => claim.Type == "id")?.Value;
            var firstNameClaim = principal.Claims.FirstOrDefault(claim => claim.Type == "firstName")?.Value;
            var lastNameClaim = principal.Claims.FirstOrDefault(claim => claim.Type == "lastName")?.Value;
            var emailClaim = principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(idClaim) || string.IsNullOrEmpty(firstNameClaim) || string.IsNullOrEmpty(lastNameClaim) || !int.TryParse(idClaim, out var id) || string.IsNullOrEmpty(emailClaim))
            {
                return null;
            }

            return new GuestDto(id, firstNameClaim, lastNameClaim, emailClaim);
        }
    }
}
