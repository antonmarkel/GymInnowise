﻿namespace GymInnowise.Shared.Authorization.Dtos.RequestModels
{
    public class RegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
