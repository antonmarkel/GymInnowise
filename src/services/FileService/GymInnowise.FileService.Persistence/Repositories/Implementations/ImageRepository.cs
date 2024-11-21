using GymInnowise.FileService.Persistence.Data;
using GymInnowise.FileService.Persistence.Models;
using GymInnowise.FileService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.FileService.Persistence.Repositories.Implementations
{
    public class ImageRepository(FileServiceDbContext _context) : IFileMetadataRepository<ImageMetadataEntity>
    {
        public async Task<Guid> CreateFileMetadataAsync(ImageMetadataEntity image,
            CancellationToken cancellationToken = default)
        {
            await _context.Images.AddAsync(image, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return image.Id;
        }

        public async Task<ImageMetadataEntity?> GetFileMetadataByIdAsync(Guid fileId,
            CancellationToken cancellationToken = default)
        {
            var image = await _context.Images.FirstOrDefaultAsync(im => im.Id == fileId, cancellationToken);

            return image;
        }
    }
}