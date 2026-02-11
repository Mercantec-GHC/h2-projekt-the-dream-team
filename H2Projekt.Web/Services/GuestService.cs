using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace H2Projekt.Web.Services
{
    public class GuestService
    {
        private readonly ProtectedLocalStorage _protectedLocalStorage;

        public GuestService(ProtectedLocalStorage protectedLocalStorage)
        {
            _protectedLocalStorage = protectedLocalStorage;
        }

        private GuestDto? _selectedGuest;

        public GuestDto? SelectedGuest { get { return _selectedGuest; } }

        public event EventHandler<GuestDto?> OnSelectedGuestChanged;

        public async Task<GuestDto?> GetGuestFromLocalStorage()
        {
            ProtectedBrowserStorageResult<GuestDto> result = await _protectedLocalStorage.GetAsync<GuestDto>("selectedGuest");

            if (!result.Success)
            {
                return null;
            }

            if (result.Value is null)
            {
                return null;
            }

            await SelectGuest(result.Value);

            return result.Value;
        }

        public async Task SelectGuest(GuestDto? guest)
        {
            _selectedGuest = guest;

            if (guest is not null)
            {
                await _protectedLocalStorage.SetAsync("selectedGuest", guest);
            }
            else
            {
                await _protectedLocalStorage.DeleteAsync("selectedGuest");
            }

            if (OnSelectedGuestChanged is not null)
            {
                OnSelectedGuestChanged(this, guest);
            }
        }
    }
}
