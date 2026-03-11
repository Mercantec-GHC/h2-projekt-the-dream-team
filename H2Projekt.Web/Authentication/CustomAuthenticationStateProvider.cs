using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace H2Projekt.Web.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private static readonly AuthenticationState anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        private readonly IJSRuntime _js;
        private readonly HttpClient _httpClient;

        public CustomAuthenticationStateProvider(IJSRuntime js, HttpClient httpClient)
        {
            _js = js;
            _httpClient = httpClient;
        }

        public async Task LoginAsync(string token)
        {
            // Store the token in local storage
            await _js.InvokeVoidAsync("localStorage.setItem", "accessToken", token);

            // Notify the authentication state has changed with the new authenticated state
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task LogoutAsync()
        {
            // Remove the token from local storage
            await _js.InvokeVoidAsync("localStorage.removeItem", "accessToken");

            // Clear the token from local storage
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Get the token from local storage
            var accessToken = await _js.InvokeAsync<string>("localStorage.getItem", "accessToken");

            // If the token is not found or is empty
            if (string.IsNullOrEmpty(accessToken))
            {
                // Return an unauthenticated state
                return anonymous;
            }

            // Parse the token and create a ClaimsPrincipal
            var claims = ParseClaimsFromJwt(accessToken);

            // Get the expiration claim from the list of claims
            var expClaim = claims.FirstOrDefault(c => c.Type == "exp");

            // If the expiration claim is found and can be parsed as a long
            if (expClaim is not null && long.TryParse(expClaim.Value, out var expSeconds))
            {
                // Create a variable for the issued at time by converting the iat claim value from seconds since the Unix epoch to a DateTimeOffset + the minutes that it takes before the token expires
                var expiresAt = DateTimeOffset.FromUnixTimeSeconds(expSeconds);

                // If the token has expired
                if (expiresAt < DateTimeOffset.UtcNow)
                {
                    // Remove the token from local storage if it has expired
                    await _js.InvokeVoidAsync("localStorage.removeItem", "accessToken");

                    // Return an unauthenticated state
                    return anonymous;
                }
            }

            // Create a new instance of the ClaimsIdentity with the parsed claims and specify the authentication type
            var claimsIdentity = new ClaimsIdentity(claims, "jwt");

            // Get the work context from the ClaimsIdentity
            var workContext = new ClaimsPrincipal(claimsIdentity);

            // Return the authenticated state with the work context
            return new AuthenticationState(workContext);
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            // Split the JWT into its three parts: header, payload, and signature
            var payload = jwt.Split('.')[1];

            // Decode the payload from Base64
            var jsonBytes = ParseBase64WithoutPadding(payload);

            // Deserialize the JSON payload into a dictionary of key-value pairs
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            // Create a list of claims from the key-value pairs
            var claims = new List<Claim>();

            // Loop through each key-value pair and add it as a claim to the list
            foreach (var kvp in keyValuePairs ?? new Dictionary<string, object>())
            {
                // Add a new claim with the key as the claim type and the value as the claim value
                claims.Add(new Claim(kvp.Key, kvp.Value.ToString()!));
            }

            // Return the list of claims
            return claims;
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            // Add padding characters to the Base64 string if necessary
            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";

                    break;
                case 3:
                    base64 += "=";

                    break;
            }

            // Decode the Base64 string into a byte array and return it
            return Convert.FromBase64String(base64);
        }
    }
}
