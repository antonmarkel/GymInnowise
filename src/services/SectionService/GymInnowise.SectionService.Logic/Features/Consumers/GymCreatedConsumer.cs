using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.Shared.RabbitMq.Events.Gym;
using GymInnowise.Shared.Sections.Redundant;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GymInnowise.SectionService.Logic.Features.Consumers
{
    public class GymCreatedConsumer : IConsumer<GymCreatedEvent>
    {
        private readonly ISender _sender;
        private readonly ILogger<GymCreatedConsumer> _logger;

        public GymCreatedConsumer(ISender sender, ILogger<GymCreatedConsumer> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<GymCreatedEvent> context)
        {
            var createdGym = context.Message.CreatedGym;
            var gym = new Gym
            {
                Name = createdGym.Name,
                PrimaryId = context.Message.GymId,
                ThumbnailId = createdGym.ThumbnailId,
            };
            await _sender.Send(new CreateRedundantCommand<Gym>(gym));
            _logger.LogInformation("Event was consumed {event}", nameof(GymCreatedEvent));
        }
    }
}
