﻿using GymInnowise.FileService.Persistence.Models.Base;

namespace GymInnowise.FileService.Persistence.Repositories.Interfaces
{
    public interface IFileMetadataRepository<T> where T : MetadataEntityBase
    {
        Task<Guid> CreateFileMetadataAsync(T metadata);
        Task<T?> GetFileMetadataByIdAsync(Guid fileId);
    }
}