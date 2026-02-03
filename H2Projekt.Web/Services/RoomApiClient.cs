using H2Projekt.Domain;

namespace H2Projekt.Web.Services
{
    public sealed class RoomApiClient
    {
        private readonly HttpClient _httpClient;

        public RoomApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Room>> GetAllAsync(CancellationToken ct = default)
        {
            var rooms = await _httpClient.GetFromJsonAsync<List<Room>>("api/room/getallrooms", ct);
            return rooms ?? new List<Room>();
        }
    }
}
