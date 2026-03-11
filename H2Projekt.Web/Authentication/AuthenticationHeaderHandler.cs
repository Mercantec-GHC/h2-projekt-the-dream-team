using Microsoft.JSInterop;

namespace H2Projekt.Web.Authentication
{
    public class AuthenticationHeaderHandler : DelegatingHandler
    {
        private readonly IJSRuntime _js;

        public AuthenticationHeaderHandler(IJSRuntime js)
        {
            _js = js;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await _js.InvokeAsync<string>("localStorage.getItem", "accessToken");

            if (!string.IsNullOrEmpty(accessToken))
            {
                Console.WriteLine($"Bearer {accessToken}");

                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
