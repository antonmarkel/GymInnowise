namespace GymInnowise.Authorization.Logic.Helpers
{
    public class JwtSettings
    {
        public required string SecretKey { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
        public required int ExpiryInMinutes { get; set; }
    }
}
