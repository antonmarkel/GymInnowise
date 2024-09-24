namespace GymInnowise.UserService.Shared.Dtos.RequestModels.Creates
{
    public class CreateClientProfileRequest
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
    }
}
