using GymInnowise.Shared.User.Dtos.RequestModels.Updates;

namespace GymInnowise.Shared.RabbitMq.Events.Profiles
{
    public class CoachProfileStatusUpdatedEvent
    {
        public required UpdateCoachProfileStatusRequest UpdateClientProfileStatusRequest { get; set; }
    }
}
