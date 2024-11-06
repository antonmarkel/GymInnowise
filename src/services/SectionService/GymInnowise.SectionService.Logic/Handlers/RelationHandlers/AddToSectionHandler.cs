using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Logic.Features.Mappers.Interfaces;
using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Interfaces;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GymInnowise.SectionService.Logic.Handlers.RelationHandlers
{
    public class
        AddToSectionHandler<TRelationEntity, TEntity, TRelation> : IRequestHandler<AddToSectionCommand<TRelation>,
        OneOf<Success, NotFound, Error<string>>>
        where TRelation : class, ISectionRelation
        where TRelationEntity : class, TRelation, IJoinEntity, ITimeStampedModel
        where TEntity : class, IEntity
    {
        private readonly ISectionRelationRepository<TRelationEntity> _relationRepository;
        private readonly ISectionRepository _sectionRepository;
        private readonly IRedundantRepository<TEntity> _entityRepository;
        private readonly IMapper<TRelation, TRelationEntity> _relationMapper;

        public AddToSectionHandler(ISectionRelationRepository<TRelationEntity> relationRepository,
            ISectionRepository sectionRepository, IRedundantRepository<TEntity> entityRepository,
            IMapper<TRelation, TRelationEntity> relationMapper)
        {
            _relationRepository = relationRepository;
            _sectionRepository = sectionRepository;
            _entityRepository = entityRepository;
            _relationMapper = relationMapper;
        }

        public async Task<OneOf<Success, NotFound, Error<string>>> Handle(AddToSectionCommand<TRelation> request,
            CancellationToken cancellationToken)
        {
            var relation = request.Relation;
            if (!(await _entityRepository.ExistsByIdAsync(relation.RelatedId, cancellationToken) &&
                  await _sectionRepository.ExistsByIdAsync(relation.SectionId, cancellationToken)))
            {
                return new NotFound();
            }

            var entity = _relationMapper.Map(relation);
            if (await _relationRepository.ExistsAsync(entity, cancellationToken))
            {
                return new Error<string>("This relation already exists!");
            }

            entity.AddedOnUtc = DateTime.UtcNow;
            await _relationRepository.AddAsync(entity, cancellationToken);

            return new Success();
        }
    }
}
