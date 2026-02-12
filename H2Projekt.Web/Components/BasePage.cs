using H2Projekt.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace H2Projekt.Web
{
    public class BasePage : ComponentBase, IDisposable
    {
        [Inject]
        public GuestService GuestService { get; set; }

        [Inject]
        public IJSRuntime JS { get; set; }

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

            await JS.InvokeVoidAsync("initDataTable");
        }

        private void OnSelectedGuestChanged(object? sender, GuestDto? guest) => InvokeAsync(StateHasChanged);

        public void Dispose()
        {
            GuestService.OnSelectedGuestChanged -= OnSelectedGuestChanged;
        }
    }
}
