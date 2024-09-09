using GymInnowise.UserService.Persistence.Models.Abstract;
using GymInnowise.UserService.Shared.Enums;

namespace GymInnowise.UserService.Persistence.Models
{
    public class CoachProfileEntity : ProfileEntity
    {
        public DateTime HiredAt { get; set; }
        public decimal CostPerHour { get; set; }
        public CoachStatus CoachStatus { get; set; }
    }
}
