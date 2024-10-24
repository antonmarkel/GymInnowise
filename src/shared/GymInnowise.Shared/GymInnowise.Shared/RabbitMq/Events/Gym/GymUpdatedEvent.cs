using GymInnowise.Shared.Gym.Dtos.Abstract;

namespace GymInnowise.Shared.RabbitMq.Events.Gym
{
    public class GymUpdatedEvent
    {
        public required Guid GymId { get; set; }
        public required GymDetailsDtoBase UpdatedGym { get; set; }
    }
}
