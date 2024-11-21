using GymInnowise.Shared.User.Dtos.RequestModels.Updates;

namespace GymInnowise.Shared.RabbitMq.Events.Profiles
{
    public class ClientProfileUpdatedEvent
    {
        public required Guid AccountId { get; set; }
        public required UpdateClientProfileRequest UpdateProfileRequest { get; set; }
    }
}
