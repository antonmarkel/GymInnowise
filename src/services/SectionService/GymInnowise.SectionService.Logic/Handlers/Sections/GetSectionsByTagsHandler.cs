using GymInnowise.SectionService.Logic.Queries;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Base;
using MediatR;

namespace GymInnowise.SectionService.Logic.Handlers.Sections
{
    public class GetSectionsByTagsHandler : IRequestHandler<GetSectionsByTagsQuery, IReadOnlyList<SectionBase>>
    {
        private readonly ISectionRepository _sectionRepository;

        public GetSectionsByTagsHandler(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public async Task<IReadOnlyList<SectionBase>> Handle(GetSectionsByTagsQuery request,
            CancellationToken cancellationToken)
        {
            var sections = await _sectionRepository.GetSectionsByTagsAsync(request.Tags ?? [], cancellationToken);

            return sections;
        }
    }
}
