namespace GymInnowise.Shared.Authorization.Dtos.RequestModels
{
    public class UpdateRolesRequest
    {
        public IEnumerable<string> Roles { get; set; } = [];
    }
}
