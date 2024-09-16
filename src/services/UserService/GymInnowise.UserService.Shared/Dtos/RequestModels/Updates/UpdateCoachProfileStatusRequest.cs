using GymInnowise.UserService.Shared.Enums;

namespace GymInnowise.UserService.Shared.Dtos.RequestModels.Updates
{
    public class UpdateCoachProfileStatusRequest : UpdateClientProfileStatusRequest
    {
        public CoachStatus CoachStatus { get; set; }
    }
}
