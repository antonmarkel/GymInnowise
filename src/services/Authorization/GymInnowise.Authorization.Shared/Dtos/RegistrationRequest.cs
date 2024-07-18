namespace GymInnowise.Authorization.Shared.Dtos
{
    public class RegistrationRequest
    {
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
