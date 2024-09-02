
namespace GymInnowise.UserService.Shared.Dtos.RequestModels
{
    public class CreateCoachProfileRequest
    {
        public Guid AccountId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public decimal CostPerHour { get; set; }
    }
}
