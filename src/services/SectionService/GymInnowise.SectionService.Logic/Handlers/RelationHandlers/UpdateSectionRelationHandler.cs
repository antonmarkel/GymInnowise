using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
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
        UpdateSectionRelationHandler<TRelationEntity, TRelation> : IRequestHandler<
        UpdateSectionRelationCommand<TRelation>,
        OneOf<Success, NotFound>>
        where TRelation : class, ISectionRelation
        where TRelationEntity : class, TRelation, IJoinEntity, new()
    {
        private readonly ISectionRelationRepository<TRelationEntity> _relationRepository;
        private readonly IMapper<TRelation, TRelationEntity> _relationMapper;
        private readonly ILogger<UpdateSectionRelationHandler<TRelationEntity, TRelation>> _logger;

        public UpdateSectionRelationHandler(ISectionRelationRepository<TRelationEntity> relationRepository,
            IMapper<TRelation, TRelationEntity> relationMapper,
            ILogger<UpdateSectionRelationHandler<TRelationEntity, TRelation>> logger)
        {
            _relationRepository = relationRepository;
            _relationMapper = relationMapper;
            _logger = logger;
        }

        public async Task<OneOf<Success, NotFound>> Handle(UpdateSectionRelationCommand<TRelation> request,
            CancellationToken cancellationToken)
        {
            var entity = _relationMapper.Map(request.UpdatedData);
            if (!await _relationRepository.ExistsAsync(entity, cancellationToken))
            {
                _logger.LogWarning("Relation was not found {sectionId} {relatedId}", request.UpdatedData.SectionId,
                    request.UpdatedData.RelatedId);

                return new NotFound();
            }

            await _relationRepository.UpdateAsync(entity, cancellationToken);
            _logger.LogInformation("Relation was successfully updated {sectionId} {relatedId}",
                request.UpdatedData.SectionId, request.UpdatedData.RelatedId);

            return new Success();
        }
    }
}
