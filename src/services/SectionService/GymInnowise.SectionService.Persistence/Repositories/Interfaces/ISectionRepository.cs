using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.Shared.Sections.Base;

namespace GymInnowise.SectionService.Persistence.Repositories.Interfaces
{
    public interface ISectionRepository
    {
        Task<SectionEntity?> GetSectionPreviewByIdAsync(Guid sectionId,
            CancellationToken cancellationToken);

        Task<SectionEntity?> GetSectionIncludeReferencesByIdAsync(Guid sectionId,
            CancellationToken cancellationToken);

        Task<IReadOnlyList<SectionEntity>> GetSectionsByTagsAsync(string[] tags,
            CancellationToken cancellationToken);

        Task UpdateSectionByIdAsync(Guid sectionId, SectionBase updateData,
            CancellationToken cancellationToken);

        Task CreateSectionAsync(SectionEntity entity, CancellationToken cancellationToken);
    }
}
