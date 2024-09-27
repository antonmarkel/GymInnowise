using GymInnowise.FileService.Persistence.Models;
using GymInnowise.Shared.Blob.Dtos.Base;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.FileService.Persistence.Data
{
    public class FileServiceDbContext : DbContext
    {
        public DbSet<DocumentMetadataEntity> Documents { get; set; }
        public DbSet<ImageMetadataEntity> Images { get; set; }

        public FileServiceDbContext(DbContextOptions<FileServiceDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureFileMetadataEntity(modelBuilder);
        }

        private static void ConfigureFileMetadataEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MetadataBase>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.FileName)
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(f => f.ContentType)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(f => f.Format)
                    .IsRequired()
                    .HasMaxLength(10);
                entity.Property(f => f.FileSize)
                    .IsRequired();
                entity.Property(f => f.CreateAt)
                    .IsRequired();
                entity.Property(f => f.UploadedBy)
                    .IsRequired();
            });
        }
    }
}