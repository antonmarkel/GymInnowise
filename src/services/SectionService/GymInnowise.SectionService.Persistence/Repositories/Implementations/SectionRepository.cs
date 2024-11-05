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

        public async Task<SectionEntity?> GetSectionPreviewByIdAsync(Guid sectionId,
            CancellationToken cancellationToken)
        {
            var entity = await
                _context.Sections.FirstOrDefaultAsync(ent => ent.Id == sectionId, cancellationToken);

            return entity;
        }

        public async Task<SectionEntity?> GetSectionIncludeReferencesByIdAsync(Guid sectionId,
            CancellationToken cancellationToken)
        {
            var entity = await _context.Sections.Include(ent => ent.Coaches)
                .Include(ent => ent.Gyms)
                .Include(ent => ent.Members)
                .FirstOrDefaultAsync(ent => ent.Id == sectionId, cancellationToken);

            return entity;
        }

        public async Task<IReadOnlyList<SectionEntity>> GetSectionsByTagsAsync(string[] tags,
            CancellationToken cancellationToken)
        {
            var sections = await _context.Sections.AsNoTracking()
                .Where(ent => ent.Tags.Any(tag => tags.Contains(tag)))
                .ToListAsync(cancellationToken);

            return sections;
        }

        public async Task UpdateSectionByIdAsync(Guid sectionId, SectionBase updateData,
            CancellationToken cancellationToken)
        {
            await _context.Sections.Where(ent => ent.Id == sectionId)
                .ExecuteUpdateAsync(section =>
                        section.SetProperty(sect => sect.Name, updateData.Name)
                            .SetProperty(sect => sect.CostPerTraining, updateData.CostPerTraining)
                            .SetProperty(sect => sect.Tags, updateData.Tags)
                            .SetProperty(sect => sect.Description, updateData.Description)
                            .SetProperty(sect => sect.IsActive, updateData.IsActive),
                    cancellationToken: cancellationToken);
        }

        public async Task CreateSectionAsync(SectionEntity entity, CancellationToken cancellationToken)
        {
            await _context.Sections.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}