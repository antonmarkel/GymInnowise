using GymInnowise.Shared.Sections.Base;

namespace GymInnowise.Shared.RabbitMq.Events.Sections
{
    public class SectionCreatedEvent
    {
        public Guid SectionId { get; set; }
        public required SectionBase CreatedSection { get; set; }
    }
}
