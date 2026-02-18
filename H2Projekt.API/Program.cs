using FluentValidation;
using H2Projekt.Application.Handlers.Bookings;
using H2Projekt.Application.Handlers.Guests;
using H2Projekt.Application.Handlers.Rooms;
using H2Projekt.Application.Interfaces;
using H2Projekt.Application.Validators.Bookings;
using H2Projekt.Application.Validators.Rooms;
using H2Projekt.Domain;
using H2Projekt.Infrastructure;
using H2Projekt.Infrastructure.Repositories;
using H2Projekt.ServiceDefaults;
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
// - Bookings
builder.Services.AddScoped<GetAllBookingsHandler>();
builder.Services.AddScoped<GetBookingByIdHandler>();
builder.Services.AddScoped<CreateBookingHandler>();
builder.Services.AddScoped<UpdateBookingHandler>();
builder.Services.AddScoped<DeleteBookingHandler>();
// - Guests
builder.Services.AddScoped<GetAllGuestsHandler>();
builder.Services.AddScoped<GetGuestByIdHandler>();
builder.Services.AddScoped<CreateGuestHandler>();
builder.Services.AddScoped<UpdateGuestHandler>();
builder.Services.AddScoped<DeleteGuestHandler>();
builder.Services.AddScoped<GetGuestByEmailHandler>();
// - Rooms
builder.Services.AddScoped<GetAllRoomsHandler>();
builder.Services.AddScoped<GetRoomByNumberHandler>();
builder.Services.AddScoped<CreateRoomHandler>();
builder.Services.AddScoped<UpdateRoomHandler>();
builder.Services.AddScoped<DeleteRoomHandler>();
// - Room types
builder.Services.AddScoped<GetAllRoomTypesHandler>();
builder.Services.AddScoped<CreateRoomTypeHandler>();
builder.Services.AddScoped<UpdateRoomTypeHandler>();
builder.Services.AddScoped<DeleteRoomTypeHandler>();

// Validators
builder.Services.AddScoped<IValidator<Booking>, BookingValidator>();
builder.Services.AddScoped<IValidator<Guest>, GuestValidator>();
builder.Services.AddScoped<IValidator<Room>, RoomValidator>();
builder.Services.AddScoped<IValidator<RoomType>, RoomTypeValidator>();

// Repositories
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();

// CORS Policy
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

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllers();

app.MapDefaultEndpoints();

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
