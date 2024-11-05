using GymInnowise.SectionService.Logic.Commands;
using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Interfaces;
using MediatR;

namespace GymInnowise.SectionService.Logic.Handlers.RelationHandlers
{
    public class
        RemoveFromSectionHandler<TRelationEntity, TRelation> : IRequestHandler<RemoveFromSectionCommand<TRelation>>
        where TRelation : class, ISectionRelation
        where TRelationEntity : class, TRelation, IJoinEntity, new()
    {
        private readonly ISectionRelationRepository<TRelationEntity> _relationRepository;

        public RemoveFromSectionHandler(ISectionRelationRepository<TRelationEntity> relationRepository)
        {
            _relationRepository = relationRepository;
        }

        public async Task Handle(RemoveFromSectionCommand<TRelation> request, CancellationToken cancellationToken)
        {
            var entity = new TRelationEntity
            {
                RelatedId = request.RelatedId,
                SectionId = request.SectionId
            };
            await _relationRepository.RemoveAsync(entity, cancellationToken);
        }
    }
}
