using GymInnowise.UserService.Shared.Enums;

namespace GymInnowise.UserService.Persistence.Models
{
    public class CoachProfileModel : ClientProfileModel
    {
        public DateTime HiredAt { get; set; }
        public decimal CostPerHour { get; set; }
        public CoachStatus CoachStatus { get; set; }
    }
}
