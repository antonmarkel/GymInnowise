using FakeItEasy;
using GymInnowise.FileService.Configuration.Blob;
using GymInnowise.FileService.Logic.Services;
using GymInnowise.Shared.Files.Dtos.Base;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GymInnowise.FileService.UnitTests.Services
{
    public class ThumbnailServiceTests
    {
        private readonly ThumbnailSettings _thumbnailSettings;
        private readonly ILogger<ThumbnailService> _logger;
        private readonly ThumbnailService _thumbnailService;

        public ThumbnailServiceTests()
        {
            _logger = A.Fake<ILogger<ThumbnailService>>();
            _thumbnailSettings = new ThumbnailSettings()
            {
                ContentType = "type",
                Format = "format",
                MaxFileSizeWithoutThumbnail = 500,
                ThumbnailHeight = 500,
                ThumbnailWidth = 500,
            };
            var thumbnailOptions = A.Fake<IOptions<ThumbnailSettings>>();
            A.CallTo(() => thumbnailOptions.Value).Returns(_thumbnailSettings);
            _thumbnailService = new ThumbnailService(thumbnailOptions, _logger);
        }

        [Fact]
        public async Task GenerateThumbnailAsync_NotNecessary_ReturnsNotNecessary()
        {
            //Arrange
            var stream = A.Fake<Stream>();
            A.CallTo(() => stream.Length).Returns(_thumbnailSettings.MaxFileSizeWithoutThumbnail - 1);
            var metadata = new ImageMetadata()
            {
                FileName = "fileName",
                ContentType = "contentType",
                Format = "format"
            };

            //Act
            var result = await _thumbnailService.GenerateThumbnailAsync(stream, metadata);

            //Assert
            Assert.True(result.IsT1);
        }
    }
}