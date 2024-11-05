using GymInnowise.SectionService.Configuration;
using GymInnowise.SectionService.Persistence.Entities;
using GymInnowise.SectionService.Persistence.Repositories.Interfaces;
using GymInnowise.Shared.Sections.Base;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace GymInnowise.SectionService.Persistence.Repositories.Cached
{
    public class SectionCachedRepository : ISectionRepository
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ISectionRepository _decorated;
        private readonly CacheSettings _cacheSettings;

        public SectionCachedRepository(ISectionRepository sectionRepository, IMemoryCache memoryCache,
            IOptions<CacheSettings> cacheSettings)
        {
            _decorated = sectionRepository;
            _memoryCache = memoryCache;
            _cacheSettings = cacheSettings.Value;
        }

        public async Task<SectionEntity?> GetSectionPreviewByIdAsync(Guid sectionId, CancellationToken cancellationToken)
        {
            var key = $"section-preview{sectionId}";

            return await _memoryCache.GetOrCreateAsync(key, async factory =>
                {
                    factory.AbsoluteExpirationRelativeToNow =
                        TimeSpan.FromMinutes(_cacheSettings.AbsoluteExpirationInMinutes);
                    factory.SlidingExpiration = TimeSpan.FromMinutes(_cacheSettings.NotUsedExpirationInMinutes);

                    return await _decorated.GetSectionPreviewByIdAsync(sectionId, cancellationToken);
                }
            );
        }

        public async Task<SectionEntity?> GetSectionIncludeReferencesByIdAsync(Guid sectionId, CancellationToken cancellationToken)
        {
            var key = $"section-include{sectionId}";

            return await _memoryCache.GetOrCreateAsync(key, async factory =>
                {
                    factory.AbsoluteExpirationRelativeToNow =
                        TimeSpan.FromMinutes(_cacheSettings.AbsoluteExpirationInMinutes);
                    factory.SlidingExpiration = TimeSpan.FromMinutes(_cacheSettings.NotUsedExpirationInMinutes);

                    return await _decorated.GetSectionIncludeReferencesByIdAsync(sectionId, cancellationToken);
                }
            );
        }

        public async Task<IReadOnlyList<SectionEntity>> GetSectionsByTagsAsync(string[] tags, CancellationToken cancellationToken)
        {
            return await _decorated.GetSectionsByTagsAsync(tags, cancellationToken);
        }

        public async Task UpdateSectionByIdAsync(Guid sectionId, SectionBase updateData, CancellationToken cancellationToken)
        {
            await _decorated.UpdateSectionByIdAsync(sectionId, updateData, cancellationToken);
        }

        public async Task CreateSectionAsync(SectionEntity entity, CancellationToken cancellationToken)
        {
            await _decorated.CreateSectionAsync(entity, cancellationToken);
        }
    }
}
