using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.SectionService.Persistence.Entities.Base;

namespace GymInnowise.SectionService.Persistence.Repositories.Interfaces
{
    public interface ISectionRelatedRepository<TEntity>
        where TEntity : class, IEntity
    {
        Task AddAsync(SectionEntity sectionEntity, TEntity relatedEntity);
        Task RemoveAsync(SectionEntity sectionEntity, TEntity relatedEntity);
    }
}
