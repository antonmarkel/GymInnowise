namespace GymInnowise.Authorization.Persistence.Models.Previews
{
    public class AccountPreview
    {
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public string[] Roles { get; set; } = Array.Empty<string>();
    }
}
