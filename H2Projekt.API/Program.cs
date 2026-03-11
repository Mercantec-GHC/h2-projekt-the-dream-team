using FluentValidation;
using H2Projekt.API;
using H2Projekt.Application.Commands.Authentication.Validators;
using H2Projekt.Application.Commands.Bookings.Validators;
using H2Projekt.Application.Commands.Guests.Validators;
using H2Projekt.Application.Commands.Rooms.Validators;
using H2Projekt.Application.Handlers.Authentication;
using H2Projekt.Application.Handlers.Bookings;
using H2Projekt.Application.Handlers.Guests;
using H2Projekt.Application.Handlers.Rooms;
using H2Projekt.Application.Interfaces;
using H2Projekt.Infrastructure;
using H2Projekt.Infrastructure.Authentication;
using H2Projekt.Infrastructure.Repositories;
using H2Projekt.ServiceDefaults;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add controllers to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
});

// Add command validators to the container.
// - Authentication
builder.Services.AddValidatorsFromAssemblyContaining<LoginValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();
// - Bookings
builder.Services.AddValidatorsFromAssemblyContaining<CreateBookingValidator>();
// - Guests
builder.Services.AddValidatorsFromAssemblyContaining<CreateGuestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateGuestValidator>();
// - Rooms
builder.Services.AddValidatorsFromAssemblyContaining<CreateRoomValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateRoomValidator>();
// - Room types
builder.Services.AddValidatorsFromAssemblyContaining<CreateRoomTypeValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetAvailableRoomTypesForStayValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateRoomTypeValidator>();
// - Room discounts
builder.Services.AddValidatorsFromAssemblyContaining<CreateRoomDiscountValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateRoomDiscountValidator>();

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Configure JSON options to use string enums.
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// EF Core
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Db")));


// Add Authentication
var jwtSection = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSection["SecretKey"]!);

builder.Services.AddScoped<IJWTTokenService, JWTTokenService>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSection["Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });

builder.Services.AddAuthorization(options =>
{
    // You can add policies later, e.g. AdminOnly
});

// Application handlers
// - Authentication
builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<RegisterHandler>();
// - Bookings
builder.Services.AddScoped<GetAllBookingsHandler>();
builder.Services.AddScoped<GetBookingByIdHandler>();
builder.Services.AddScoped<CreateBookingHandler>();
builder.Services.AddScoped<AssignRoomToBookingHandler>();
builder.Services.AddScoped<DeleteBookingHandler>();
builder.Services.AddScoped<GetBookingsOverviewHandler>();
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
builder.Services.AddScoped<GetAvailableRoomTypesForStayHandler>();
builder.Services.AddScoped<CreateRoomTypeHandler>();
builder.Services.AddScoped<UpdateRoomTypeHandler>();
builder.Services.AddScoped<DeleteRoomTypeHandler>();
// - Room discounts
builder.Services.AddScoped<GetAllRoomDiscountsHandler>();
builder.Services.AddScoped<CreateRoomDiscountHandler>();
builder.Services.AddScoped<UpdateRoomDiscountHandler>();
builder.Services.AddScoped<DeleteRoomDiscountHandler>();

// Repositories
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();

builder.Services.AddControllers();

// CORS Policy
builder.Services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Run();