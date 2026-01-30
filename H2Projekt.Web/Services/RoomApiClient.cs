using System.Net.Http.Json;

namespace H2Projekt.Web.Services
{
    public sealed class RoomApiClient
    {
        private readonly HttpClient _http;
        public RoomApiClient(HttpClient http) => _http = http;

        public async Task<List<RoomDto>> GetAllAsync(CancellationToken ct = default)
        {
            var rooms = await _http.GetFromJsonAsync<List<RoomDto>>("api/room/getallrooms", ct);
            return rooms ?? new List<RoomDto>();
        }
    }

    public sealed class RoomDto
    {
        public int Id { get; set; }
        public string Number { get; set; } = "";
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
