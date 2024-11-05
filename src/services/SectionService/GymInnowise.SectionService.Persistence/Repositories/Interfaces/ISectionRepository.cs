using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.Shared.Sections.Base;

namespace GymInnowise.SectionService.Persistence.Repositories.Interfaces
{
    public interface ISectionRepository
    {
        Task<SectionEntity?> GetSectionPreviewByIdAsync(Guid sectionId, bool asNoTracking = false,
            CancellationToken cancellationToken = default);

        Task<SectionEntity?> GetSectionIncludeReferencesByIdAsync(Guid sectionId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<SectionEntity>> GetSectionsByTagsAsync(string[] tags,
            CancellationToken cancellationToken = default);

        Task UpdateSectionByIdAsync(Guid sectionId, SectionBase updateData,
            CancellationToken cancellationToken = default);

        Task CreateSectionAsync(SectionEntity entity, CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
