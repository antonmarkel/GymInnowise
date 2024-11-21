using GymInnowise.SectionService.Persistence.Data;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Base;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.SectionService.Persistence.Repositories.Implementations
{
    public class SectionRepository : ISectionRepository
    {
        private readonly SectionDbContext _context;

        public SectionRepository(SectionDbContext context)
        {
            _context = context;
        }

        public async Task<SectionEntity?> GetSectionPreviewByIdAsync(Guid sectionId, bool asNoTracking = false,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Sections.AsQueryable();
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var entity = await
                query.FirstOrDefaultAsync(ent => ent.PrimaryId == sectionId, cancellationToken);

            return entity;
        }

        public async Task<SectionEntity?> GetSectionIncludeReferencesByIdAsync(Guid sectionId,
            CancellationToken cancellationToken = default)
        {
            var entity = await _context.Sections.AsNoTracking()
                .Include(ent => ent.Coaches)
                .ThenInclude(rel => rel.Coach)
                .Include(ent => ent.Gyms)
                .ThenInclude(rel => rel.Gym)
                .Include(ent => ent.Members)
                .ThenInclude(rel => rel.Member)
                .FirstOrDefaultAsync(ent => ent.PrimaryId == sectionId, cancellationToken);

            return entity;
        }

        public async Task<IReadOnlyList<SectionEntity>> GetSectionsByTagsAsync(string[]? tags,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Sections.AsNoTracking();
            if (tags != null && tags.Length != 0)
            {
                query = query.Where(ent => ent.Tags.Any(tag => tags.Contains(tag)));
            }

            var sections = await query.ToListAsync(cancellationToken);

            return sections;
        }

        public async Task UpdateSectionByIdAsync(Guid sectionId, SectionBase updateData,
            CancellationToken cancellationToken = default)
        {
            await _context.Sections.Where(ent => ent.PrimaryId == sectionId)
                .ExecuteUpdateAsync(section =>
                        section.SetProperty(sect => sect.Name, updateData.Name)
                            .SetProperty(sect => sect.CostPerTraining, updateData.CostPerTraining)
                            .SetProperty(sect => sect.Tags, updateData.Tags)
                            .SetProperty(sect => sect.Description, updateData.Description)
                            .SetProperty(sect => sect.IsActive, updateData.IsActive)
                            .SetProperty(sect => sect.ThumbnailId, updateData.ThumbnailId),
                    cancellationToken: cancellationToken = default);
        }

        public async Task CreateSectionAsync(SectionEntity entity, CancellationToken cancellationToken = default)
        {
            await _context.Sections.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Sections.AnyAsync(sect => sect.PrimaryId == id, cancellationToken);
        }
    }
}