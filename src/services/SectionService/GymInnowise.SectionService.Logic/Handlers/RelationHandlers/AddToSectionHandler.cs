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
        OneOf<Success, NotFound>>
        where TRelation : class, ISectionRelation
        where TRelationEntity : class, TRelation, IJoinEntity
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

        public async Task<OneOf<Success, NotFound>> Handle(AddToSectionCommand<TRelation> request,
            CancellationToken cancellationToken)
        {
            var mentorship = request.Relation;
            var profileExistTask = _entityRepository.ExistsByIdAsync(mentorship.RelatedId, cancellationToken);
            var sectionExistTask = _sectionRepository.ExistsByIdAsync(mentorship.SectionId, cancellationToken);
            if (!await profileExistTask || !await sectionExistTask)
            {
                return new NotFound();
            }

            await _relationRepository.AddAsync(_relationMapper.Map(mentorship), cancellationToken);

            return new Success();
        }
    }
}
