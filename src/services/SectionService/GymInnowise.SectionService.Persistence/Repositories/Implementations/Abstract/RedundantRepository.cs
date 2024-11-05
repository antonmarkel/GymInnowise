using GymInnowise.SectionService.Persistence.Data;
using GymInnowise.SectionService.Persistence.Entities.Base;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.SectionService.Persistence.Repositories.Implementations.Abstract;

public abstract class RedundantRepository<TEntity> : IRedundantRepository<TEntity>
    where TEntity : class, IEntity

{
    private readonly SectionDbContext _context;

    protected RedundantRepository(SectionDbContext context)
    {
        _context = context;
    }

    public async Task UploadAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateByIdAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var doesExist = await _context.Set<TEntity>().AnyAsync(ent => ent.Id == id, cancellationToken);

        return doesExist;
    }
}
