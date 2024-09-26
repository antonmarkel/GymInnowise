using GymInnowise.FileService.Persistence.Data;
using GymInnowise.FileService.Persistence.Models;
using GymInnowise.FileService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.FileService.Persistence.Repositories.Implementations
{
    public class ImageRepository(FileServiceDbContext _context) : IFileMetadataRepository<ImageEntity>
    {
        public async Task CreateFileMetadataAsync(ImageEntity image)
        {
            await _context.Images.AddAsync(image);
            await _context.SaveChangesAsync();
        }

        public async Task<ImageEntity?> GetFileMetadataByIdAsync(Guid fileId)
        {
            var image = await _context.Images.FirstOrDefaultAsync(im => im.Id == fileId);

            return image;
        }
    }
}
