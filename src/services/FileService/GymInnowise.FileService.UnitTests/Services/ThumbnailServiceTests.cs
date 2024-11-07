using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using GymInnowise.FileService.Configuration.Blob;
using GymInnowise.FileService.Logic.Services;
using GymInnowise.Shared.Files.Dtos.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace GymInnowise.FileService.UnitTests.Services;

public class ThumbnailServiceTests
{
    private readonly IFixture _fixture;
    private readonly ThumbnailSettings _thumbnailSettings;
    private readonly ThumbnailService _thumbnailService;

    public ThumbnailServiceTests()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _fixture.Freeze<Mock<ILogger<ThumbnailService>>>();
        var thumbnailOptions = _fixture.Freeze<Mock<IOptions<ThumbnailSettings>>>();
        _thumbnailSettings = _fixture.Create<ThumbnailSettings>();

        thumbnailOptions.Setup(o => o.Value).Returns(_thumbnailSettings);
        _thumbnailService = _fixture.Create<ThumbnailService>();
    }

    [Fact]
    public async Task GenerateThumbnailAsync_NotNecessary_ReturnsNotNecessary()
    {
        var stream = _fixture.Create<Mock<Stream>>();
        stream.Setup(s => s.Length).Returns(_thumbnailSettings.MaxFileSizeWithoutThumbnail - 1);
        var metadata = _fixture.Create<ImageMetadata>();

        // Act
        var result = await _thumbnailService.GenerateThumbnailAsync(stream.Object, metadata);

        // Assert
        result.IsT1.Should().BeTrue();
    }
}