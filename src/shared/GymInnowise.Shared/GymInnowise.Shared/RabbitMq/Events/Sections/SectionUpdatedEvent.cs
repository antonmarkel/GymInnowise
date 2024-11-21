using GymInnowise.Shared.Sections.Base;

namespace GymInnowise.Shared.RabbitMq.Events.Sections
{
    public class SectionUpdatedEvent
    {
        public Guid SectionId { get; set; }
        public required SectionBase UpdatedSection { get; set; }
    }
}
