using GymInnowise.Shared.User.Enums;
using GymInnowise.UserService.Persistence.Models.Abstract;

namespace GymInnowise.UserService.Persistence.Models
{
    public class CoachProfileEntity : ProfileEntity
    {
        public DateTime HiredAt { get; set; }
        public Guid[] DocumentFileIds { get; set; } = [];
        public decimal CostPerHour { get; set; }
        public CoachStatus CoachStatus { get; set; }
    }
}