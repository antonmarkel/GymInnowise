using GymInnowise.FileService.Persistence.Data;
using GymInnowise.FileService.Persistence.Models;
using GymInnowise.FileService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.FileService.Persistence.Repositories.Implementations
{
    public class DocumentRepository(FileServiceDbContext _context) : IFileMetadataRepository<DocumentMetadataEntity>
    {
        public async Task CreateFileMetadataAsync(DocumentMetadataEntity document)
        {
            await _context.Documents.AddAsync(document);
            await _context.SaveChangesAsync();
        }

        public async Task<DocumentMetadataEntity?> GetFileMetadataByIdAsync(Guid docId)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(doc => doc.Id == docId);

            return document;
        }
    }
}
