using GymInnowise.Shared.Gym.Dtos.Abstract;

namespace GymInnowise.Shared.RabbitMq.Events.Gym
{
    public class GymCreatedEvent
    {
        public required Guid GymId { get; set; }
        public required GymDetailsDtoBase CreatedGym { get; set; }
    }
}
