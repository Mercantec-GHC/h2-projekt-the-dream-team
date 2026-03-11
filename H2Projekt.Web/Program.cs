using H2Projekt.Web;
using H2Projekt.Web.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


// Add Authorization Message Handler
builder.Services.AddScoped<AuthenticationHeaderHandler>();

// Register ApiClient with authorization handler
builder.Services
    .AddHttpClient<ApiClient>(client =>
    {
        // This URL uses "https+http://" to indicate HTTPS is preferred over HTTP.
        // Learn more about service discovery scheme resolution at https://aka.ms/dotnet/sdschemes.
        client.BaseAddress = new("https+http://apiservice");
    })
    .AddHttpMessageHandler<AuthenticationHeaderHandler>()
    .AddTypedClient((httpClient) =>
    {
        var apiClient = new ApiClient(httpClient);

        apiClient.ReadResponseAsString = true;

        return apiClient;
    });

// Add Authorization
builder.Services.AddAuthorizationCore();

// Add Authentication Services
builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

await builder.Build().RunAsync();
