using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.Shared.Authorization;
using GymInnowise.Shared.RabbitMq.Events.Profiles;
using GymInnowise.Shared.Sections.Redundant;
using MassTransit;
using MediatR;

namespace GymInnowise.SectionService.Logic.Features.Consumers
{
    public class CoachProfileCreateConsumer : IConsumer<CoachProfileCreatedEvent>
    {
        private readonly ISender _sender;

        public CoachProfileCreateConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<CoachProfileCreatedEvent> context)
        {
            var createdCoach = context.Message.CreatedProfile;
            var profile = new Profile
            {
                PrimaryId = context.Message.AccountId,
                FirstName = createdCoach.FirstName,
                LastName = createdCoach.LastName,
                Role = Roles.Coach,
                ThumbnailId = createdCoach.ThumbnailId
            };
            await _sender.Send(new CreateRedundantCommand<Profile>(profile));
        }
    }
}
