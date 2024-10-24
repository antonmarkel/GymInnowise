using GymInnowise.Shared.User.Dtos.Profiles;

namespace GymInnowise.Shared.RabbitMq.Events.Profiles
{
    public class ClientProfileCreatedEvent
    {
        public required Guid AccountId { get; set; }
        public required ClientProfile CreatedProfile { get; set; }
    }
}
