using GymInnowise.Shared.User.Dtos.RequestModels.Updates;

namespace GymInnowise.Shared.RabbitMq.Events.Profiles
{
    public class ClientProfileStatusUpdatedEvent
    {
        public required Guid AccountId { get; set; }
        public required UpdateClientProfileStatusRequest UpdateClientProfileStatusRequest { get; set; }
    }
}
