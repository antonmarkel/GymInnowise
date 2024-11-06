using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.Shared.Authorization;
using GymInnowise.Shared.RabbitMq.Events.Profiles;
using GymInnowise.Shared.Sections.Redundant;
using MassTransit;
using MediatR;

namespace GymInnowise.SectionService.Logic.Features.Consumers
{
    public class ClientProfileCreatedConsumer : IConsumer<ClientProfileCreatedEvent>
    {
        private readonly ISender _sender;

        public ClientProfileCreatedConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<ClientProfileCreatedEvent> context)
        {
            var eventProfile = context.Message.CreatedProfile;
            var profile = new Profile()
            {
                PrimaryId = context.Message.AccountId,
                FirstName = eventProfile.FirstName,
                LastName = eventProfile.LastName,
                Role = Roles.Client,
                ThumbnailId = eventProfile.ThumbnailId
            };
            await _sender.Send(new CreateRedundantCommand<Profile>(profile));
        }
    }
}
