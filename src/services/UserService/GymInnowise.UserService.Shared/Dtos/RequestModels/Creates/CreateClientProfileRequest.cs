namespace GymInnowise.UserService.Shared.Dtos.RequestModels.Creates
{
    public class CreateClientProfileRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
    }
}
