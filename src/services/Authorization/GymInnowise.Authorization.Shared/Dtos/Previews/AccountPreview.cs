namespace GymInnowise.Authorization.Shared.Dtos.Previews
{
    public class AccountPreview
    {
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
