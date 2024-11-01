using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using GymInnowise.FileService.Configuration.Blob;
using GymInnowise.FileService.Logic.Services;
using GymInnowise.FileService.Persistence.Models;
using GymInnowise.FileService.Persistence.Repositories.Interfaces;
using GymInnowise.FileService.Persistence.Services.Interfaces;
using Microsoft.Extensions.Options;
using Moq;

namespace GymInnowise.FileService.UnitTests.Services
{
    public class ImageServiceTests
    {
        private readonly IFixture _fixture;
        private readonly Mock<IFileMetadataRepository<ImageMetadataEntity>> _repo;
        private readonly Mock<IBlobService> _blobService;
        private readonly ContainerSettings _containerSettings;
        private readonly ImageService _imageService;
        private readonly CancellationToken _cancellationToken;

        public ImageServiceTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());

            _repo = _fixture.Freeze<Mock<IFileMetadataRepository<ImageMetadataEntity>>>();
            _blobService = _fixture.Freeze<Mock<IBlobService>>();
            _containerSettings = new ContainerSettings { ImageContainer = "Images" };
            var containerOptions = _fixture.Freeze<Mock<IOptions<ContainerSettings>>>();
            containerOptions.Setup(c => c.Value).Returns(_containerSettings);

            _imageService = _fixture.Create<ImageService>();
            _cancellationToken = new CancellationTokenSource().Token;
        }

        [Fact]
        public async Task DownloadAsync_MetadataNotFound_ReturnsMetadataNotFound()
        {
            // Arrange
            var fileId = _fixture.Create<Guid>();
            var stream = _fixture.Create<Stream>();

            _repo.Setup(r => r.GetFileMetadataByIdAsync(fileId, _cancellationToken))
                .ReturnsAsync((ImageMetadataEntity?)null);

            _blobService.Setup(b =>
                    b.DownloadAsync(fileId.ToString(), _containerSettings.ImageContainer, _cancellationToken))
                .ReturnsAsync(stream);

            // Act
            var result = await _imageService.DownloadAsync(fileId, _cancellationToken);

            // Assert
            result.IsT1.Should().BeTrue();
        }

        [Fact]
        public async Task DownloadAsync_FileNotFound_ReturnsFileNotFound()
        {
            //Arrange
            Guid fileId = _fixture.Create<Guid>();
            _repo
                .Setup(b => b.GetFileMetadataByIdAsync(fileId, _cancellationToken))
                .ReturnsAsync(_fixture.Create<ImageMetadataEntity>());

            _blobService.Setup(b =>
                    b.DownloadAsync(fileId.ToString(), _containerSettings.ImageContainer, _cancellationToken))
                .ReturnsAsync((Stream?)null);

            //Act
            var result = await _imageService.DownloadAsync(fileId, _cancellationToken);

            //Assert
            result.IsT2.Should().BeTrue();
        }

        [Fact]
        public async Task DownloadAsync_Success_ReturnsFileResult()
        {
            // Arrange
            var fileId = _fixture.Create<Guid>();
            var stream = _fixture.Create<Stream>();
            var metadata = _fixture.Create<ImageMetadataEntity>();

            _repo.Setup(r => r.GetFileMetadataByIdAsync(fileId, _cancellationToken))
                .ReturnsAsync(metadata);

            _blobService.Setup(b =>
                    b.DownloadAsync(fileId.ToString(), _containerSettings.ImageContainer, _cancellationToken))
                .ReturnsAsync(stream);

            // Act
            var result = await _imageService.DownloadAsync(fileId, _cancellationToken);

            // Assert
            result.IsT0.Should().BeTrue();
        }

        [Fact]
        public async Task GetMetadataByIdAsync_MetadataNotFound_ReturnsMetadataNotFound()
        {
            // Arrange
            var fileId = _fixture.Create<Guid>();

            _repo.Setup(r => r.GetFileMetadataByIdAsync(fileId, _cancellationToken))
                .ReturnsAsync((ImageMetadataEntity?)null);

            // Act
            var result = await _imageService.GetMetadataByIdAsync(fileId, _cancellationToken);

            // Assert
            result.IsT1.Should().BeTrue();
        }

        [Fact]
        public async Task GetMetadataByIdAsync_Success_ReturnsImageMetadata()
        {
            // Arrange
            var fileId = _fixture.Create<Guid>();
            var metadata = _fixture.Create<ImageMetadataEntity>();
            _repo.Setup(r => r.GetFileMetadataByIdAsync(fileId, _cancellationToken))
                .ReturnsAsync(metadata);

            // Act
            var result = await _imageService.GetMetadataByIdAsync(fileId, _cancellationToken);

            // Assert
            result.IsT0.Should().BeTrue();
        }
    }
}