namespace GymInnowise.UserService.Shared.Dtos.RequestModels.Creates
{
    public class CreateCoachProfileRequest
    {
        public Guid AccountId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public decimal CostPerHour { get; set; }
    }
}
