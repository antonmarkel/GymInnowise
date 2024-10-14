using GymInnowise.FileService.Persistence.Data;
using GymInnowise.FileService.Persistence.Models;
using GymInnowise.FileService.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.FileService.Persistence.Repositories.Implementations
{
    public class DocumentRepository(FileServiceDbContext _context) : IFileMetadataRepository<DocumentMetadataEntity>
    {
        public async Task<Guid> CreateFileMetadataAsync(DocumentMetadataEntity document,
            CancellationToken cancellationToken = default)
        {
            await _context.Documents.AddAsync(document, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return document.Id;
        }

        public async Task<DocumentMetadataEntity?> GetFileMetadataByIdAsync(Guid docId,
            CancellationToken cancellationToken = default)
        {
            var document = await _context.Documents.FirstOrDefaultAsync(doc => doc.Id == docId, cancellationToken);

            return document;
        }
    }
}