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

public class DocumentServiceTests
{
    private readonly IFixture _fixture;
    private readonly Mock<IFileMetadataRepository<DocumentMetadataEntity>> _repo;
    private readonly Mock<IBlobService> _blobService;
    private readonly ContainerSettings _containerSettings;
    private readonly DocumentService _documentService;
    private readonly CancellationToken _cancellationToken;

    public DocumentServiceTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _repo = _fixture.Freeze<Mock<IFileMetadataRepository<DocumentMetadataEntity>>>();
        _blobService = _fixture.Freeze<Mock<IBlobService>>();
        _containerSettings = new ContainerSettings { DocumentContainer = "documents" };
        var containerOptions = _fixture.Freeze<Mock<IOptions<ContainerSettings>>>();
        containerOptions.Setup(c => c.Value).Returns(_containerSettings);
        _documentService = _fixture.Create<DocumentService>();
        _cancellationToken = new CancellationTokenSource().Token;
    }

    [Fact]
    public async Task DownloadAsync_MetadataNotFound_ReturnsMetadataNotFound()
    {
        // Arrange
        var fileId = _fixture.Create<Guid>();
        var stream = _fixture.Create<Stream>();

        _repo.Setup(r => r.GetFileMetadataByIdAsync(fileId, _cancellationToken))
            .ReturnsAsync((DocumentMetadataEntity?)null);

        _blobService.Setup(b =>
                b.DownloadAsync(fileId.ToString(), _containerSettings.DocumentContainer, _cancellationToken))
            .ReturnsAsync(stream);

        // Act
        var result = await _documentService.DownloadAsync(fileId, _cancellationToken);

        // Assert
        result.IsT1.Should().BeTrue();
    }

    [Fact]
    public async Task DownloadAsync_FileNotFound_ReturnsFileNotFound()
    {
        // Arrange
        var fileId = _fixture.Create<Guid>();
        var metadata = _fixture.Create<DocumentMetadataEntity>();

        _repo.Setup(r => r.GetFileMetadataByIdAsync(fileId, _cancellationToken))
            .ReturnsAsync(metadata);

        _blobService.Setup(b =>
                b.DownloadAsync(fileId.ToString(), _containerSettings.DocumentContainer, _cancellationToken))
            .ReturnsAsync((Stream?)null);

        // Act
        var result = await _documentService.DownloadAsync(fileId, cancellationToken: _cancellationToken);

        // Assert
        result.IsT2.Should().BeTrue();
    }

    [Fact]
    public async Task DownloadAsync_Success_ReturnsFileResult()
    {
        // Arrange
        var fileId = _fixture.Create<Guid>();
        var stream = _fixture.Create<Stream>();
        var metadata = _fixture.Create<DocumentMetadataEntity>();

        _repo.Setup(r => r.GetFileMetadataByIdAsync(fileId, _cancellationToken))
            .ReturnsAsync(metadata);

        _blobService.Setup(b =>
                b.DownloadAsync(fileId.ToString(), _containerSettings.DocumentContainer, _cancellationToken))
            .ReturnsAsync(stream);

        // Act
        var result = await _documentService.DownloadAsync(fileId, _cancellationToken);

        // Assert
        result.IsT0.Should().BeTrue();
    }

    [Fact]
    public async Task GetMetadataByIdAsync_MetadataNotFound_ReturnsMetadataNotFound()
    {
        // Arrange
        var fileId = _fixture.Create<Guid>();

        _repo.Setup(r => r.GetFileMetadataByIdAsync(fileId, _cancellationToken))
            .ReturnsAsync((DocumentMetadataEntity?)null);

        // Act
        var result = await _documentService.GetMetadataByIdAsync(fileId, _cancellationToken);

        // Assert
        result.IsT1.Should().BeTrue();
    }

    [Fact]
    public async Task GetMetadataByIdAsync_Success_ReturnsDocumentMetadata()
    {
        // Arrange
        var fileId = _fixture.Create<Guid>();
        var metadata = _fixture.Create<DocumentMetadataEntity>();

        _repo.Setup(r => r.GetFileMetadataByIdAsync(fileId, _cancellationToken))
            .ReturnsAsync(metadata);

        // Act
        var result = await _documentService.GetMetadataByIdAsync(fileId, _cancellationToken);

        // Assert
        result.IsT0.Should().BeTrue();
    }
}