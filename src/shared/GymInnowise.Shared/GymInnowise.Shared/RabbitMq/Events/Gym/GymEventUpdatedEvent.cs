using GymInnowise.Shared.Gym.Dtos.Abstract;

namespace GymInnowise.Shared.RabbitMq.Events.Gym
{
    public class GymEventUpdatedEvent
    {
        public required Guid EventId { get; set; }
        public required GymEventDtoBase UpdatedGymEvent { get; set; }
    }
}
