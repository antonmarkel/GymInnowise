using GymInnowise.UserService.Shared.Enums;

namespace GymInnowise.UserService.Shared.Dtos.ResponseModels.Gets
{
    public class GetCoachProfileResponse : GetClientProfileResponse
    {
        public DateTime HiredAt { get; set; }
        public decimal CostPerHour { get; set; }
        public CoachStatus CoachStatus { get; set; }
    }
}
