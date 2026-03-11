using H2Projekt.Application.Interfaces;
using H2Projekt.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace H2Projekt.Infrastructure.Authentication
{
    public class JWTTokenService : IJWTTokenService
    {
        private readonly IConfiguration _configuration;

        public JWTTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(Guest guest)
        {
            // Get the JWT settings from the configuration
            var jwtSettings = _configuration.GetSection("Jwt");

            // Create a variable for the token's expiration time by adding the specified number of minutes to the current UTC time
            var expires = DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpirationMinutes"]!));

            // Define the claims to be included in the token
            var claims = new List<Claim>
            {
                new Claim("id", guest.Id.ToString()),
                new Claim("firstName", guest.FirstName),
                new Claim("lastName", guest.LastName),
                new Claim("email", guest.Email),
                new Claim(ClaimTypes.Role, guest.Id <= 2 ? "Admin" : "User"), // Important to use ClaimTypes for this one
                new Claim("iat", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim("exp", new DateTimeOffset(expires).ToUnixTimeSeconds().ToString()),
            };

            // Generate a security key from the secret key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));

            // Create signing credentials using the security key and HMAC SHA256 algorithm
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Create a new JWT token with the specified issuer, audience, claims, expiration time, and signing credentials
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims,
                expires,
                signingCredentials: credentials
            );

            // Return an AuthResponseDto containing the serialized token and its expiration time
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
