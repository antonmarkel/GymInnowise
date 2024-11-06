using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.RabbitMq.Events.Sections;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace GymInnowise.SectionService.Logic.Handlers.Sections
{
    public class UpdateSectionHandler : IRequestHandler<UpdateSectionCommand, OneOf<Success, NotFound>>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IPublishEndpoint _publisher;
        private readonly ILogger<UpdateSectionHandler> _logger;

        public UpdateSectionHandler(ISectionRepository sectionRepository, IPublishEndpoint publisher,
            ILogger<UpdateSectionHandler> logger)
        {
            _sectionRepository = sectionRepository;
            _publisher = publisher;
            _logger = logger;
        }

        public async Task<OneOf<Success, NotFound>> Handle(UpdateSectionCommand request,
            CancellationToken cancellationToken)
        {
            if (!await _sectionRepository.ExistsByIdAsync(request.SectionId, cancellationToken))
            {
                _logger.LogWarning("Section was not found {sectioId}!", request.SectionId);

                return new NotFound();
            }

            await _sectionRepository.UpdateSectionByIdAsync(request.SectionId, request.UpdateData, cancellationToken);

            var udpatedEvent = new SectionUpdatedEvent
            {
                UpdatedSection = request.UpdateData,
                SectionId = request.SectionId
            };
            await _publisher.Publish(udpatedEvent);
            _logger.LogInformation("Section was successfully updated! {sectionId}", request.SectionId);

            return new Success();
        }
    }
}
