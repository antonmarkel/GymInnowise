using GymInnowise.Shared.User.Dtos.RequestModels.Updates;

namespace GymInnowise.Shared.RabbitMq.Events.Profiles
{
    public class ClientProfileUpdatedEvent
    {
        public required UpdateClientProfileRequest UpdateProfileRequest { get; set; }
    }
}
