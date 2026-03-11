namespace H2Projekt.Application.Dto.Authentication
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public DateTimeOffset ExpiresAtUtc { get; set; }
    }
}
