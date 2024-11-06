using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.RabbitMq.Events.Sections;
using MassTransit;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GymInnowise.SectionService.Logic.Handlers.Sections
{
    public class UpdateSectionHandler : IRequestHandler<UpdateSectionCommand, OneOf<Success, NotFound>>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IPublishEndpoint _publisher;

        public UpdateSectionHandler(ISectionRepository sectionRepository, IPublishEndpoint publisher)
        {
            _sectionRepository = sectionRepository;
            _publisher = publisher;
        }

        public async Task<OneOf<Success, NotFound>> Handle(UpdateSectionCommand request,
            CancellationToken cancellationToken)
        {
            if (!await _sectionRepository.ExistsByIdAsync(request.SectionId, cancellationToken))
            {
                return new NotFound();
            }

            await _sectionRepository.UpdateSectionByIdAsync(request.SectionId, request.UpdateData, cancellationToken);

            var udpatedEvent = new SectionUpdatedEvent
            {
                UpdatedSection = request.UpdateData,
                SectionId = request.SectionId
            };
            await _publisher.Publish(udpatedEvent);

            return new Success();
        }
    }
}
