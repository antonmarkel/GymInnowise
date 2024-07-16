namespace GymInnowise.Authorization.Shared.Dtos.Previews
{
    public class RolePreview
    {
        public required string RoleName { get; set; }
        public IEnumerable<string> Clients { get; set; } = new List<string>();
    }
}
