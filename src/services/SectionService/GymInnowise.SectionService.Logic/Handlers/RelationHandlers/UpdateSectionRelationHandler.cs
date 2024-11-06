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
        UpdateSectionRelationHandler<TRelationEntity, TRelation> : IRequestHandler<
        UpdateSectionRelationCommand<TRelation>,
        OneOf<Success, NotFound>>
        where TRelation : class, ISectionRelation
        where TRelationEntity : class, TRelation, IJoinEntity, new()
    {
        private readonly ISectionRelationRepository<TRelationEntity> _relationRepository;
        private readonly IMapper<TRelation, TRelationEntity> _relationMapper;

        public UpdateSectionRelationHandler(ISectionRelationRepository<TRelationEntity> relationRepository,
            IMapper<TRelation, TRelationEntity> relationMapper)
        {
            _relationRepository = relationRepository;
            _relationMapper = relationMapper;
        }

        public async Task<OneOf<Success, NotFound>> Handle(UpdateSectionRelationCommand<TRelation> request,
            CancellationToken cancellationToken)
        {
            var entity = _relationMapper.Map(request.UpdatedData);
            if (!await _relationRepository.ExistsAsync(entity, cancellationToken))
            {
                return new NotFound();
            }

            await _relationRepository.UpdateAsync(entity, cancellationToken);

            return new Success();
        }
    }
}
