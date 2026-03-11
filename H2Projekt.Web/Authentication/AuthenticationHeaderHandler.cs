using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net;
using System.Net.Http.Json;

namespace H2Projekt.Web.Authentication
{
    public class AuthenticationHeaderHandler : DelegatingHandler
    {
        private readonly IJSRuntime _js;
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public AuthenticationHeaderHandler(IJSRuntime js, HttpClient httpClient, NavigationManager navigationManager)
        {
            _js = js;
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Get the access token from local storage
            var accessToken = await _js.InvokeAsync<string>("localStorage.getItem", "accessToken");

            // If the access token is not null or empty
            if (!string.IsNullOrEmpty(accessToken))
            {
                Console.WriteLine($"Bearer {accessToken}");

                // Add the access token to the Authorization header of the request
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }

            // Send the request to the server and get the response
            var response = await base.SendAsync(request, cancellationToken);

            // If the response status code is Unauthorized and the access token is not null or empty
            if (!string.IsNullOrEmpty(accessToken) && response is not null && response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Get the refresh token from local storage
                var refreshToken = await _js.InvokeAsync<string>("localStorage.getItem", "refreshToken");

                // Call the Refresh endpoint to get a new token 
                var newTokenResponse = await _httpClient.PostAsJsonAsync("/api/Auth/Refresh", refreshToken, cancellationToken);

                // If the new token is null, return
                if (newTokenResponse is null || !newTokenResponse.IsSuccessStatusCode)
                {
                    return response;
                }

                // Read the new token response content as a TokenResponse object
                var newToken = await newTokenResponse.Content.ReadFromJsonAsync<AuthResponseDto>();

                // If the new token is null, return
                if (newToken is null)
                {
                    return response;
                }

                // Store the new access token in local storage
                await _js.InvokeVoidAsync("localStorage.setItem", "accessToken", newToken.AccessToken);

                // Store the new refresh token in local storage
                await _js.InvokeVoidAsync("localStorage.setItem", "refreshToken", newToken.RefreshToken);

                // Update the Authorization header with the new access token
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", newToken.AccessToken);

                // Dispose the original response to free up resources
                response.Dispose();

                // Redirect to the current page to trigger a re-render and update the UI with the new authentication state
                _navigationManager.NavigateTo(_navigationManager.Uri, true);

                // Retry the original request with the new access token
                response = await base.SendAsync(request, cancellationToken);
            }

            // Return the original response if the status code is not Unauthorized or if the access token is null or empty
            return response!;
        }
    }
}
