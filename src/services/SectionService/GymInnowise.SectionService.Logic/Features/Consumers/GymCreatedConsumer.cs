using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.Shared.RabbitMq.Events.Gym;
using GymInnowise.Shared.Sections.Redundant;
using MassTransit;
using MediatR;

namespace GymInnowise.SectionService.Logic.Features.Consumers
{
    public class GymCreatedConsumer : IConsumer<GymCreatedEvent>
    {
        private readonly ISender _sender;

        public GymCreatedConsumer(ISender sender)
        {
            _sender = sender;
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
        }
    }
}
