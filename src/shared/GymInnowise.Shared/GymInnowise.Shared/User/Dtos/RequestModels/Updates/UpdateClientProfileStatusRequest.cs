using GymInnowise.Shared.User.Enums;

namespace GymInnowise.Shared.User.Dtos.RequestModels.Updates
{
    public class UpdateClientProfileStatusRequest
    {
        public ClientStatus AccountStatus { get; set; }
        public string? StatusNotes { get; set; }
        public DateTime? ExpectedReturnDate { get; set; }
    }
}