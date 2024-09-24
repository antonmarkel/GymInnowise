namespace GymInnowise.Authorization.Shared.Dtos.RequestModels
{
    public class UpdateRolesRequest
    {
        public IEnumerable<string> Roles { get; set; } = [];
    }
}
