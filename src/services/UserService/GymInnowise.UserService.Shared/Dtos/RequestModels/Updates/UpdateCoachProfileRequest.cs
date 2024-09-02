using GymInnowise.UserService.Shared.Enums;

namespace GymInnowise.UserService.Shared.Dtos.RequestModels.Updates
{
    public class UpdateCoachProfileRequest
    {
        public Guid AccountId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public List<TagEnum> Tags { get; set; } = [TagEnum.ToAdd];
    }
}