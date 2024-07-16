namespace GymInnowise.Authorization.Logic.Dtos
{
    public class AccountLoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
