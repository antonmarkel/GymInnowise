using GymInnowise.SectionService.Logic.Queries;
using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Interfaces;
using MediatR;
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

        public GetSectionRelationHandler(ISectionRelationRepository<TRelationEntity> relationRepository)
        {
            _relationRepository = relationRepository;
        }

        public async Task<OneOf<TRelation, NotFound>> Handle(GetSectionRelationQuery<TRelation> request,
            CancellationToken cancellationToken)
        {
            var entity = await _relationRepository.GetAsync(request.SectionId, request.RelatedId, cancellationToken);
            if (entity is null)
            {
                return new NotFound();
            }

            return entity;
        }
    }
}
