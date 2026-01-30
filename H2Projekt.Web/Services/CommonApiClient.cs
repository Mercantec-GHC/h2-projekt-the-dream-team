using H2Projekt.Domain;

namespace H2Projekt.Web.Services
{
    public sealed class CommonApiClient
    {
        private readonly HttpClient _httpClient;

        public CommonApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Rule>> GetRulesAsync(CancellationToken cancellationToken = default)
        {
            var rules = await _httpClient.GetFromJsonAsync<List<Rule>>("api/Common/GetRules", cancellationToken);

            return rules ?? new List<Rule>();
        }
    }
}