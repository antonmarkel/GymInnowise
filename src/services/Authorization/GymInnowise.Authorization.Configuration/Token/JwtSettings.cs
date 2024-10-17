﻿namespace GymInnowise.Authorization.Configuration.Token
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpiryInMinutes { get; set; }
        public int RefreshExpiryInMinutes { get; set; }
    }
}