using GymInnowise.SectionService.Logic.Queries;
using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using OneOf.Types;

namespace GymInnowise.SectionService.Logic.Handlers.RelationHandlers
{
    public class
        GetSectionRelationHandler<TRelation, TRelationEntity> : IRequestHandler<GetSectionRelationQuery<TRelation>,
        OneOf<TRelation, NotFound>>
        where TRelation : class, ISectionRelation
        where TRelationEntity : class, TRelation, IJoinEntity

    {
        private readonly ISectionRelationRepository<TRelationEntity> _relationRepository;
        private readonly ILogger<GetSectionRelationHandler<TRelation, TRelationEntity>> _logger;

        public GetSectionRelationHandler(ISectionRelationRepository<TRelationEntity> relationRepository,
            ILogger<GetSectionRelationHandler<TRelation, TRelationEntity>> logger)
        {
            _relationRepository = relationRepository;
            _logger = logger;
        }

        public async Task<OneOf<TRelation, NotFound>> Handle(GetSectionRelationQuery<TRelation> request,
            CancellationToken cancellationToken)
        {
            var entity = await _relationRepository.GetAsync(request.SectionId, request.RelatedId, cancellationToken);
            if (entity is null)
            {
                _logger.LogWarning("Relation was not found {sectionId} {relatedId}", request.SectionId,
                    request.RelatedId);

                return new NotFound();
            }

            return entity;
        }
    }
}
