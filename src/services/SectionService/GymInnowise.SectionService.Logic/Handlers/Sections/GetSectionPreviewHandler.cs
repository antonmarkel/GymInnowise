using GymInnowise.SectionService.Logic.Queries;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Base;
using MediatR;
using OneOf;
using OneOf.Types;

namespace GymInnowise.SectionService.Logic.Handlers.Sections
{
    public class GetSectionPreviewHandler : IRequestHandler<GetSectionPreviewQuery, OneOf<SectionBase, NotFound>>
    {
        private readonly ISectionRepository _sectionRepository;

        public GetSectionPreviewHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<OneOf<SectionBase, NotFound>> Handle(GetSectionPreviewQuery request,
            CancellationToken cancellationToken)
        {
            var entity =
                await _sectionRepository.GetSectionPreviewByIdAsync(request.SectionId, asNoTracking: true,
                    cancellationToken);
            if (entity is null)
            {
                return new NotFound();
            }

            return entity;
        }
    }
}
