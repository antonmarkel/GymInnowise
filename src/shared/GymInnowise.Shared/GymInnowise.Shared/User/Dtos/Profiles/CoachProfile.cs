using GymInnowise.Shared.User.Enums;

namespace GymInnowise.Shared.User.Dtos.Profiles
{
    public class CoachProfile : ClientProfile
    {
        public DateTime HiredAt { get; set; }
        public decimal CostPerHour { get; set; }
        public CoachStatus CoachStatus { get; set; }
    }
}
