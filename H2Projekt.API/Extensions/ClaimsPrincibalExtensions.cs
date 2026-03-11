using H2Projekt.API.Context;
using H2Projekt.Domain.Enums;
using System.Security.Claims;

namespace H2Projekt.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static WorkContext? GetWorkContextOrNull(this ClaimsPrincipal principal)
        {
            if (principal?.Identity is null || !principal.Identity.IsAuthenticated)
            {
                return null;
            }

            var idClaim = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            var firstNameClaim = principal.FindFirstValue("firstName");
            var lastNameClaim = principal.FindFirstValue("lastName");
            var emailClaim = principal.FindFirstValue(ClaimTypes.Email);

            if (idClaim is null || firstNameClaim is null || lastNameClaim is null || !int.TryParse(idClaim, out var id) || emailClaim is null)
            {
                return null;
            }

            return new WorkContext(id, firstNameClaim, lastNameClaim, emailClaim);
        }
    }
}
