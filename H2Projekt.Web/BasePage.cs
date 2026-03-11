using H2Projekt.Web.Authentication;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace H2Projekt.Web
{
    public class BasePage : ComponentBase
    {
        [Inject]
        protected CustomAuthenticationStateProvider AuthService { get; set; }

        protected GuestDto? WorkContext { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            var state = await AuthService.GetAuthenticationStateAsync();

            WorkContext = GetWorkContextOrNull(state.User);
        }

        private GuestDto? GetWorkContextOrNull(ClaimsPrincipal principal)
        {
            if (principal?.Identity is null || !principal.Identity.IsAuthenticated)
            {
                return null;
            }

            var idClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var firstNameClaim = principal.FindFirst("firstName")?.Value;
            var lastNameClaim = principal.FindFirst("lastName")?.Value;
            var emailClaim = principal.FindFirst(ClaimTypes.Email)?.Value;

            if (idClaim is null || firstNameClaim is null || lastNameClaim is null || !int.TryParse(idClaim, out var id) || emailClaim is null)
            {
                return null;
            }

            return new GuestDto()
            {
                Id = id,
                FirstName = firstNameClaim,
                LastName = lastNameClaim,
                Email = emailClaim,
            };
        }
    }
}
