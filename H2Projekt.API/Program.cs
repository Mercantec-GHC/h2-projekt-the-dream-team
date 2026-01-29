using H2Projekt.Application.Handlers;
using H2Projekt.Application.Interfaces;
using H2Projekt.Infrastructure;
using H2Projekt.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add controllers to the container.
builder.Services.AddControllers();

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// EF Core
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Db")));

// Application handlers
builder.Services.AddScoped<CreateRoomHandler>();

// Repositories
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapControllers();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
