using GymInnowise.Shared.User.Enums;

namespace GymInnowise.Shared.User.Dtos.ResponseModels.Gets
{
    public class GetCoachProfileResponse : GetClientProfileResponse
    {
        public DateTime HiredAt { get; set; }
        public decimal CostPerHour { get; set; }
        public CoachStatus CoachStatus { get; set; }
    }
}