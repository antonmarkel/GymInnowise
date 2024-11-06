using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.Shared.Authorization;
using GymInnowise.Shared.RabbitMq.Events.Profiles;
using GymInnowise.Shared.Sections.Redundant;
using MassTransit;
using MediatR;

namespace GymInnowise.SectionService.Logic.Features.Consumers
{
    public class CoachProfileUpdatedConsumer : IConsumer<CoachProfileUpdatedEvent>
    {
        private readonly ISender _sender;

        public CoachProfileUpdatedConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<CoachProfileUpdatedEvent> context)
        {
            var updatedCoach = context.Message.UpdateProfileRequest;
            var profile = new Profile
            {
                PrimaryId = context.Message.AccountId,
                FirstName = updatedCoach.FirstName,
                LastName = updatedCoach.LastName,
                Role = Roles.Coach,
                ThumbnailId = updatedCoach.ThumbnailId
            };
            await _sender.Send(new UpdateRedundantCommand<Profile>(profile));
        }
    }
}
