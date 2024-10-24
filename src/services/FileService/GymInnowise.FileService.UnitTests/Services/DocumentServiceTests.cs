using FakeItEasy;
using GymInnowise.FileService.Configuration.Blob;
using GymInnowise.FileService.Logic.Services;
using GymInnowise.FileService.Persistence.Models;
using GymInnowise.FileService.Persistence.Repositories.Interfaces;
using GymInnowise.FileService.Persistence.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GymInnowise.FileService.UnitTests.Services
{
    public class DocumentServiceTests
    {
        private readonly IFileMetadataRepository<DocumentMetadataEntity> _repo;
        private readonly IBlobService _blobService;
        private readonly ILogger<DocumentService> _logger;
        private readonly ContainerSettings _containerSettings;
        private readonly DocumentService _documentService;
        private readonly CancellationToken _cacnelletaionToken;

        public DocumentServiceTests()
        {
            _repo = A.Fake<IFileMetadataRepository<DocumentMetadataEntity>>();
            _blobService = A.Fake<IBlobService>();
            _logger = A.Fake<ILogger<DocumentService>>();
            var containerOptions = A.Fake<IOptions<ContainerSettings>>();
            _containerSettings = new ContainerSettings()
            {
                DocumentContainer = "documents",
            };
            A.CallTo(() => containerOptions.Value).Returns(_containerSettings);
            _documentService = new DocumentService(_repo, _blobService,
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
                    _repo.GetFileMetadataByIdAsync(fileId, _cacnelletaionToken))
                .Returns(Task.FromResult<DocumentMetadataEntity?>(null));
            A.CallTo(() => _blobService.DownloadAsync(fileId.ToString(), _containerSettings.DocumentContainer,
                    _cacnelletaionToken))
                .Returns(Task.FromResult<Stream?>(stream));

            //Act
            var result = await _documentService.DownloadAsync(fileId);

            //Assert
            Assert.True(result.IsT1);
        }

        [Fact]
        public async Task DownloadAsync_FileNotFound_ReturnsFileNotFound()
        {
            //Arrange
            Guid fileId = new();
            A.CallTo(() =>
                _repo.GetFileMetadataByIdAsync(fileId, _cacnelletaionToken)).Returns(
                Task.FromResult<DocumentMetadataEntity?>(
                    new DocumentMetadataEntity()
                    {
                        FileName = "fileName",
                        ContentType = "contentType",
                        Format = "format"
                    }));
            A.CallTo(() => _blobService.DownloadAsync(fileId.ToString(), _containerSettings.DocumentContainer,
                    _cacnelletaionToken))
                .Returns(Task.FromResult<Stream?>(null));

            //Act
            var result = await _documentService.DownloadAsync(fileId, cancellationToken: _cacnelletaionToken);

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
                _repo.GetFileMetadataByIdAsync(fileId, _cacnelletaionToken)).Returns(
                Task.FromResult<DocumentMetadataEntity?>(
                    new DocumentMetadataEntity()
                    {
                        FileName = "fileName",
                        ContentType = "contentType",
                        Format = "format"
                    }));
            A.CallTo(() => _blobService.DownloadAsync(fileId.ToString(), _containerSettings.DocumentContainer,
                    _cacnelletaionToken))
                .Returns(Task.FromResult<Stream?>(stream));

            //Act
            var result = await _documentService.DownloadAsync(fileId);

            //Assert
            Assert.True(result.IsT0);
        }

        [Fact]
        public async Task GetMetadataByIdAsync_MetadataNotFound_ReturnsMetadataNotFound()
        {
            //Arrange
            var fileId = new Guid();
            A.CallTo(() => _repo.GetFileMetadataByIdAsync(fileId, _cacnelletaionToken))
                .Returns(Task.FromResult<DocumentMetadataEntity?>(null));

            //Act
            var result = await _documentService.GetMetadataByIdAsync(fileId);

            //Assert
            Assert.True(result.IsT1);
        }

        [Fact]
        public async Task GetMetadataByIdAsync_Success_ReturnsDocumentMetadata()
        {
            //Arrange
            var fileId = new Guid();
            A.CallTo(() => _repo.GetFileMetadataByIdAsync(fileId, _cacnelletaionToken))
                .Returns(Task.FromResult<DocumentMetadataEntity?>(new DocumentMetadataEntity()
                {
                    FileName = "fileName",
                    ContentType = "contentType",
                    Format = "format"
                }));

            //Act
            var result = await _documentService.GetMetadataByIdAsync(fileId);

            //Assert
            Assert.True(result.IsT0);
        }
    }
}

