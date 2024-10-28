using GymInnowise.Shared.User.Enums;

namespace GymInnowise.Shared.User.Dtos.RequestModels.Updates
{
    public class UpdateClientProfileRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public List<TagEnum> Tags { get; set; } = [TagEnum.ToAdd];
    }
}