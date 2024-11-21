namespace GymInnowise.Shared.RabbitMq.Events.Gym
{
    public class GymEventRemovedEvent
    {
        public required Guid EventId { get; set; }
    }
}