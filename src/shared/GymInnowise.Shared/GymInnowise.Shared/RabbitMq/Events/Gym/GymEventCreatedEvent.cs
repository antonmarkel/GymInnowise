using GymInnowise.Shared.Gym.Dtos.Abstract;

namespace GymInnowise.Shared.RabbitMq.Events.Gym
{
    public class GymEventCreatedEvent
    {
        public required Guid EventId { get; set; }
        public required GymEventDtoBase CreatedEvent { get; set; }
    }
}
