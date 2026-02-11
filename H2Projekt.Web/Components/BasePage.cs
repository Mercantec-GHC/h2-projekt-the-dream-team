using H2Projekt.Web.Services;
using Microsoft.AspNetCore.Components;

namespace H2Projekt.Web
{
    public class BasePage : ComponentBase, IDisposable
    {
        [Inject]
        public GuestService GuestService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            GuestService.OnSelectedGuestChanged += OnSelectedGuestChanged;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (!firstRender)
            {
                return;
            }

            await GuestService.GetGuestFromLocalStorage();
        }

        private void OnSelectedGuestChanged(object? sender, GuestDto? guest) => InvokeAsync(StateHasChanged);

        public void Dispose()
        {
            GuestService.OnSelectedGuestChanged -= OnSelectedGuestChanged;
        }
    }
}
