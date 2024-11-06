using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.Shared.RabbitMq.Events.Gym;
using GymInnowise.Shared.Sections.Redundant;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GymInnowise.SectionService.Logic.Features.Consumers
{
    public class GymUpdatedConsumer : IConsumer<GymUpdatedEvent>
    {
        private readonly ISender _sender;
        private readonly ILogger<GymUpdatedConsumer> _logger;

        public GymUpdatedConsumer(ISender sender, ILogger<GymUpdatedConsumer> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<GymUpdatedEvent> context)
        {
            var updatedGym = context.Message.UpdatedGym;
            var gym = new Gym
            {
                Name = updatedGym.Name,
                PrimaryId = context.Message.GymId,
                ThumbnailId = updatedGym.ThumbnailId
            };
            await _sender.Send(new UpdateRedundantCommand<Gym>(gym));
            _logger.LogInformation("Event was consumed {event}", nameof(GymUpdatedEvent));
        }
    }
}
