using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GymInnowise.SectionService.Logic.Handlers.RelationHandlers
{
    public class
        RemoveFromSectionHandler<TRelationEntity, TRelation> : IRequestHandler<RemoveFromSectionCommand<TRelation>>
        where TRelation : class, ISectionRelation
        where TRelationEntity : class, TRelation, IJoinEntity, new()
    {
        private readonly ISectionRelationRepository<TRelationEntity> _relationRepository;
        private readonly ILogger<RemoveFromSectionHandler<TRelationEntity, TRelation>> _logger;

        public RemoveFromSectionHandler(ISectionRelationRepository<TRelationEntity> relationRepository,
            ILogger<RemoveFromSectionHandler<TRelationEntity, TRelation>> logger)
        {
            _relationRepository = relationRepository;
            _logger = logger;
        }

        public async Task Handle(RemoveFromSectionCommand<TRelation> request, CancellationToken cancellationToken)
        {
            var entity = new TRelationEntity
            {
                RelatedId = request.RelatedId,
                SectionId = request.SectionId
            };
            await _relationRepository.RemoveAsync(entity, cancellationToken);
            _logger.LogInformation("Relation was removed {sectionId} {relatedId}", request.SectionId,
                request.RelatedId);
        }
    }
}
