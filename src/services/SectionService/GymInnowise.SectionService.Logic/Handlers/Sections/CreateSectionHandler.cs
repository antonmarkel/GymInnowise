using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.RabbitMq.Events.Sections;
using GymInnowise.Shared.Sections.Base;
using MassTransit;
using MediatR;

namespace GymInnowise.SectionService.Logic.Handlers.Sections
{
    public class CreateSectionHandler : IRequestHandler<CreateSectionCommand, Guid>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper<SectionBase, SectionEntity> _sectionMapper;
        private readonly IPublishEndpoint _publisher;

        public CreateSectionHandler(ISectionRepository sectionRepository,
            IMapper<SectionBase, SectionEntity> sectionMapper, IPublishEndpoint publisher)
        {
            _sectionRepository = sectionRepository;
            _sectionMapper = sectionMapper;
            _publisher = publisher;
        }

        public async Task<Guid> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            var entity = _sectionMapper.Map(request.SectionData);
            await _sectionRepository.CreateSectionAsync(entity, cancellationToken);

            var createdEvent = new SectionCreatedEvent
            {
                CreatedSection = request.SectionData,
                SectionId = entity.PrimaryId
            };
            await _publisher.Publish(createdEvent);

            return entity.PrimaryId;
        }
    }
}
