using GymInnowise.UserService.Persistence.Models.Abstract;
using GymInnowise.UserService.Shared.Enums;

namespace GymInnowise.UserService.Persistence.Models
{
    public class CoachProfile : Profile
    {
        public DateTime HiredAt { get; set; }
        public decimal CostPerHour { get; set; }
        public CoachStatus CoachStatus { get; set; }

        public override string GetTableName()
        {
            return "CoachProfiles";
        }
    }
}
