using H2Projekt.Web.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Security.Claims;

namespace H2Projekt.Web
{
    public class BasePage : ComponentBase, IDisposable
    {
        [Inject]
        protected CustomAuthenticationStateProvider AuthService { get; set; }

        [Inject]
        protected IJSRuntime JS { get; set; }

        protected GuestDto? WorkContext { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                return;
            }

            await JS.InvokeVoidAsync("initDataTable");

            await OnAccessTokenChanged(null, null);

            AuthService.OnAccessTokenChanged += OnAccessTokenChanged;

            StateHasChanged();
        }

        private async Task OnAccessTokenChanged(object? sender, string? token)
        {
            var state = await AuthService.GetAuthenticationStateAsync();

            WorkContext = GetWorkContextOrNull(state.User);

            await InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            AuthService.OnAccessTokenChanged -= OnAccessTokenChanged;
        }

        private GuestDto? GetWorkContextOrNull(ClaimsPrincipal principal)
        {
            if (principal?.Identity is null || !principal.Identity.IsAuthenticated)
            {
                return null;
            }

            var idClaim = principal.Claims.FirstOrDefault(claim => claim.Type == "id")?.Value;
            var firstNameClaim = principal.Claims.FirstOrDefault(claim => claim.Type == "firstName")?.Value;
            var lastNameClaim = principal.Claims.FirstOrDefault(claim => claim.Type == "lastName")?.Value;
            var emailClaim = principal.Claims.FirstOrDefault(claim => claim.Type == "email")?.Value;

            if (string.IsNullOrEmpty(idClaim) || string.IsNullOrEmpty(firstNameClaim) || string.IsNullOrEmpty(lastNameClaim) || !int.TryParse(idClaim, out var id) || string.IsNullOrEmpty(emailClaim))
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
