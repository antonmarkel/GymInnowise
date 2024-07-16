namespace GymInnowise.Authorization.Persistence.Models.Previews
{
    public class RolePreview
    {
        public required string RoleName { get; set; }
        public string[] Clients { get; set; } = Array.Empty<string>();

    }
}
