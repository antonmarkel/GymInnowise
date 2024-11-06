using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.Shared.RabbitMq.Events.Gym;
using GymInnowise.Shared.Sections.Redundant;
using MassTransit;
using MediatR;

namespace GymInnowise.SectionService.Logic.Features.Consumers
{
    public class GymUpdatedConsumer : IConsumer<GymUpdatedEvent>
    {
        private readonly ISender _sender;

        public GymUpdatedConsumer(ISender sender)
        {
            _sender = sender;
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
        }
    }
}
