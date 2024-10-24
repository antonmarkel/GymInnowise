using GymInnowise.Shared.User.Dtos.Profiles;

namespace GymInnowise.Shared.RabbitMq.Events.Profiles
{
    public class CoachProfileCreatedEvent
    {
        public required Guid AccountId { get; set; }
        public required CoachProfile CreatedProfile { get; set; }
    }
}
