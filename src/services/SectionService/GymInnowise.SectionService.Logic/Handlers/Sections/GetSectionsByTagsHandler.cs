using GymInnowise.SectionService.Logic.Queries;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Base;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GymInnowise.SectionService.Logic.Handlers.Sections
{
    public class GetSectionsByTagsHandler : IRequestHandler<GetSectionsByTagsQuery, IReadOnlyList<SectionBase>>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly ILogger<GetSectionsByTagsHandler> _logger;

        public GetSectionsByTagsHandler(ISectionRepository sectionRepository, ILogger<GetSectionsByTagsHandler> logger)
        {
            _sectionRepository = sectionRepository;
            _logger = logger;
        }

        public async Task<IReadOnlyList<SectionBase>> Handle(GetSectionsByTagsQuery request,
            CancellationToken cancellationToken)
        {
            var sections = await _sectionRepository.GetSectionsByTagsAsync(request.Tags ?? [], cancellationToken);
            _logger.LogInformation("Sections werer retrieved by tags: {tags}", request.Tags ?? []);

            return sections;
        }
    }
}
