using GymInnowise.Shared.User.Enums;

namespace GymInnowise.Shared.User.Dtos.Profiles
{
    public class ClientProfile
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ClientStatus AccountStatus { get; set; }
        public string? StatusNotes { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public List<TagEnum> Tags { get; set; } = [TagEnum.ToAdd];
    }
}