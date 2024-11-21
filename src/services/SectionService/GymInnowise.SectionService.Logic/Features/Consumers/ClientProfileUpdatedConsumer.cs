using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.Shared.Authorization;
using GymInnowise.Shared.RabbitMq.Events.Profiles;
using GymInnowise.Shared.Sections.Redundant;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GymInnowise.SectionService.Logic.Features.Consumers
{
    public class ClientProfileUpdatedConsumer : IConsumer<ClientProfileUpdatedEvent>
    {
        private readonly ISender _sender;
        private readonly ILogger<ClientProfileUpdatedConsumer> _logger;

        public ClientProfileUpdatedConsumer(ISender sender, ILogger<ClientProfileUpdatedConsumer> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ClientProfileUpdatedEvent> context)
        {
            var updatedProfile = context.Message.UpdateProfileRequest;
            var profile = new Profile()
            {
                PrimaryId = context.Message.AccountId,
                FirstName = updatedProfile.FirstName,
                LastName = updatedProfile.LastName,
                Role = Roles.Client,
                ThumbnailId = updatedProfile.ThumbnailId
            };
            await _sender.Send(new UpdateRedundantCommand<Profile>(profile));
            _logger.LogInformation("Event was consumer {event}", nameof(ClientProfileUpdatedConsumer));
        }
    }
}
