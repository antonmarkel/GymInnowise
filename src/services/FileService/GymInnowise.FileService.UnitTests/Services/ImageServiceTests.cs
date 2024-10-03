using FakeItEasy;
using GymInnowise.FileService.Configuration.Blob;
using GymInnowise.FileService.Logic.Interfaces;
using GymInnowise.FileService.Logic.Services;
using GymInnowise.FileService.Persistence.Models;
using GymInnowise.FileService.Persistence.Repositories.Interfaces;
using GymInnowise.FileService.Persistence.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GymInnowise.FileService.UnitTests.Services
{
    public class ImageServiceTests
    {
        private readonly IFileMetadataRepository<ImageMetadataEntity> _repo;
        private readonly IBlobService _blobService;
        private readonly ILogger<ImageService> _logger;
        private readonly IThumbnailService _thumbnailService;
        private readonly ContainerSettings _containerSettings;
        private readonly ImageService _imageService;
        private readonly CancellationToken _cacnelletaionToken;

        public ImageServiceTests()
        {
            _repo = A.Fake<IFileMetadataRepository<ImageMetadataEntity>>();
            _blobService = A.Fake<IBlobService>();
            _logger = A.Fake<ILogger<ImageService>>();
            _thumbnailService = A.Fake<IThumbnailService>();
            var containerOptions = A.Fake<IOptions<ContainerSettings>>();
            _containerSettings = new ContainerSettings()
            {
                ImageContainer = "Images",
            };
            A.CallTo(() => containerOptions.Value).Returns(_containerSettings);
            _imageService = new ImageService(_repo, _blobService, _thumbnailService,
                containerOptions, _logger);
            _cacnelletaionToken = new CancellationTokenSource().Token;
        }

        [Fact]
        public async Task DownloadAsync_MetadataNotFound_ReturnsMetadataNotFound()
        {
            //Arrange
            Guid fileId = new();
            var stream = A.Fake<Stream>();
            A.CallTo(() =>
                _repo.GetFileMetadataByIdAsync(fileId)).Returns(Task.FromResult<ImageMetadataEntity?>(null));
            A.CallTo(() => _blobService.DownloadAsync(fileId.ToString(), _containerSettings.ImageContainer,
                    _cacnelletaionToken))
                .Returns(Task.FromResult<Stream?>(stream));

            //Act
            var result = await _imageService.DownloadAsync(fileId);

            //Assert
            Assert.True(result.IsT1);
        }

        [Fact]
        public async Task DownloadAsync_FileNotFound_ReturnsFileNotFound()
        {
            //Arrange
            Guid fileId = new();
            A.CallTo(() =>
                _repo.GetFileMetadataByIdAsync(fileId)).Returns(Task.FromResult<ImageMetadataEntity?>(
                new ImageMetadataEntity()
                {
                    FileName = "fileName",
                    ContentType = "contentType",
                    Format = "format"
                }));
            A.CallTo(() => _blobService.DownloadAsync(fileId.ToString(), _containerSettings.ImageContainer,
                    _cacnelletaionToken))
                .Returns(Task.FromResult<Stream?>(null));

            //Act
            var result = await _imageService.DownloadAsync(fileId, cancellationToken: _cacnelletaionToken);

            //Assert
            Assert.True(result.IsT2);
        }

        [Fact]
        public async Task DownloadAsync_Success_ReturnsFileResult()
        {
            //Arrange
            Guid fileId = new();
            var stream = A.Fake<Stream>();
            A.CallTo(() =>
                _repo.GetFileMetadataByIdAsync(fileId)).Returns(Task.FromResult<ImageMetadataEntity?>(
                new ImageMetadataEntity()
                {
                    FileName = "fileName",
                    ContentType = "contentType",
                    Format = "format"
                }));
            A.CallTo(() => _blobService.DownloadAsync(fileId.ToString(), _containerSettings.ImageContainer,
                    _cacnelletaionToken))
                .Returns(Task.FromResult<Stream?>(stream));

            //Act
            var result = await _imageService.DownloadAsync(fileId);

            //Assert
            Assert.True(result.IsT0);
        }

        [Fact]
        public async Task GetMetadataByIdAsync_MetadataNotFound_ReturnsMetadataNotFound()
        {
            //Arrange
            var fileId = new Guid();
            A.CallTo(() => _repo.GetFileMetadataByIdAsync(fileId))
                .Returns(Task.FromResult<ImageMetadataEntity?>(null));

            //Act
            var result = await _imageService.GetMetadataByIdAsync(fileId);

            //Assert
            Assert.True(result.IsT1);
        }

        [Fact]
        public async Task GetMetadataByIdAsync_Success_ReturnsImageMetadata()
        {
            //Arrange
            var fileId = new Guid();
            A.CallTo(() => _repo.GetFileMetadataByIdAsync(fileId))
                .Returns(Task.FromResult<ImageMetadataEntity?>(new ImageMetadataEntity()
                {
                    FileName = "fileName",
                    ContentType = "contentType",
                    Format = "format"
                }));

            //Act
            var result = await _imageService.GetMetadataByIdAsync(fileId);

            //Assert
            Assert.True(result.IsT0);
        }
    }
}