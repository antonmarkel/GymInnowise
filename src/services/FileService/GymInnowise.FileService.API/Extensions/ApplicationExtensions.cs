using Azure.Storage.Blobs;
using FluentValidation.AspNetCore;
using FluentValidation;
using GymInnowise.FileService.API.Validators.FileValidators;
using GymInnowise.FileService.Configuration.Blob;
using GymInnowise.FileService.Logic.Interfaces;
using GymInnowise.FileService.Logic.Services;
using GymInnowise.FileService.Persistence.Data;
using GymInnowise.FileService.Persistence.Models;
using GymInnowise.FileService.Persistence.Repositories.Implementations;
using GymInnowise.FileService.Persistence.Repositories.Interfaces;
using GymInnowise.FileService.Persistence.Services.Implementations;
using GymInnowise.FileService.Persistence.Services.Interfaces;
using GymInnowise.Shared.Files.Dtos.Base;
using Microsoft.EntityFrameworkCore;

namespace GymInnowise.FileService.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void AddPersistenceServices(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<FileServiceDbContext>(
                options => options.UseNpgsql(connectionString));
            builder.Services.AddScoped<IFileMetadataRepository<DocumentMetadataEntity>, DocumentRepository>();
            builder.Services.AddScoped<IFileMetadataRepository<ImageMetadataEntity>, ImageRepository>();
            builder.AddBlobServices();
        }

        public static void AddBlobServices(this WebApplicationBuilder builder)
        {
            var blobConnectionString = builder.Configuration.GetConnectionString("AzuriteBlobStorage");
            builder.Services.AddSingleton(new BlobServiceClient(blobConnectionString));
            builder.Services.AddScoped<IBlobService, BlobService>();
        }

        public static void AddFileServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IThumbnailService, ThumbnailService>();
            builder.Services.AddScoped<IFileService<ImageMetadata>, ImageService>();
            builder.Services.AddScoped<IFileService<DocumentMetadata>, DocumentService>();
        }

        public static void AddValidation(this IHostApplicationBuilder builder)
        {
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<DocumentFileValidator>();
        }

        public static void AddConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<ContainerSettings>(
                builder.Configuration.GetSection("ContainerSettings"));
            builder.Services.Configure<ThumbnailSettings>(
                builder.Configuration.GetSection("ThumbnailSettings"));
            builder.Services.Configure<FileSettings>(
                builder.Configuration.GetSection("FileSettings"));
        }
    }
}