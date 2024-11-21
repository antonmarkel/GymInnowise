using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.Shared.Authorization;
using GymInnowise.Shared.RabbitMq.Events.Profiles;
using GymInnowise.Shared.Sections.Redundant;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GymInnowise.SectionService.Logic.Features.Consumers
{
    public class ClientProfileCreatedConsumer : IConsumer<ClientProfileCreatedEvent>
    {
        private readonly ISender _sender;
        private readonly ILogger<ClientProfileCreatedConsumer> _logger;

        public ClientProfileCreatedConsumer(ISender sender, ILogger<ClientProfileCreatedConsumer> logger)
        {
            _sender = sender;
            _logger = logger;
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
            _logger.LogInformation("Event was consumer {event}", nameof(ClientProfileCreatedEvent));
        }
    }
}
