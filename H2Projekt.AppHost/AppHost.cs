var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.H2Projekt_API>("api")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.H2Projekt_Web>("web")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
