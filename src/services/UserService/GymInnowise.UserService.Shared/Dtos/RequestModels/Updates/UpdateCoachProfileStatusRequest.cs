using GymInnowise.UserService.Shared.Enums;

namespace GymInnowise.UserService.Shared.Dtos.RequestModels.Updates
{
    public class UpdateCoachProfileStatusRequest
    {
        public Guid AccountId { get; set; }
        public ClientStatus AccountStatus { get; set; }
        public string? StatusNotes { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
        public CoachStatus CoachStatus { get; set; }
    }
}
