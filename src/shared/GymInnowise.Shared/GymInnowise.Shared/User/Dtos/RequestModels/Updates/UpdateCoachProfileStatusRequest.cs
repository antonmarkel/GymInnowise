using GymInnowise.Shared.User.Enums;

namespace GymInnowise.Shared.User.Dtos.RequestModels.Updates
{
    public class UpdateCoachProfileStatusRequest : UpdateClientProfileStatusRequest
    {
        public CoachStatus CoachStatus { get; set; }
    }
}