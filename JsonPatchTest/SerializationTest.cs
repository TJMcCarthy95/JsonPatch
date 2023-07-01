using System.Text.Json;
using FluentAssertions;
using JsonPatch;
using Xunit;

namespace JsonPatchTest;

public class SerializationTest
{
    [Fact]
    public void UpdateEmpty_ShouldSerializeRoundTrip()
    {
        // Arrange
        var sut = new UpdateEmptyRequest();
        var json = JsonSerializer.Serialize(sut);

        // Act
        var result = JsonSerializer.Deserialize<IUpdateRequest>(json);

        // Assert
        result.Should().BeEquivalentTo(sut);
    }

    [Fact]
    public void UpdateAddRequest_ShouldSerializeRoundTrip()
    {
        // Arrange
        var sut = new UpdateAddRequest(new[] { "string1", "string2" });
        var json = JsonSerializer.Serialize(sut);

        // Act
        var result = JsonSerializer.Deserialize<IUpdateRequest>(json);

        // Assert
        result.Should().BeEquivalentTo(sut);
    }

    [Fact]
    public void UpdateReplaceRequest_ShouldSerializeRoundTrip()
    {
        // Arrange
        var sut = new UpdateReplaceRequest(new[] { "string1", "string2" });
        var json = JsonSerializer.Serialize(sut);

        // Act
        var result = JsonSerializer.Deserialize<IUpdateRequest>(json);

        // Assert
        result.Should().BeEquivalentTo(sut);
    }

    [Fact]
    public void UpdateRemoveRequest_ShouldSerializeRoundTrip()
    {
        // Arrange
        var sut = new UpdateRemoveRequest(new[] { "string1", "string2" });
        var json = JsonSerializer.Serialize(sut);

        // Act
        var result = JsonSerializer.Deserialize<IUpdateRequest>(json);

        // Assert
        result.Should().BeEquivalentTo(sut);
    }

    [Fact]
    public void UpdateUnknownRequest_ShouldDefaultToEmpty()
    {
        // Arrange
        const string json = "{}";

        // Act
        var result = JsonSerializer.Deserialize<IUpdateRequest>(json);

        // Assert
        result.Should().BeEquivalentTo(new UpdateEmptyRequest());
    }

    [Fact]
    public void ShouldHandleNested()
    {
        // Arrange
        var sut = new BulkUpdateRequest
        {
            Ids = new[] { 1, 2 },
            Update = new UpdateRemoveRequest(new[] { "string1", "string2" })
        };
        var json = JsonSerializer.Serialize(sut);
        
        // Act
        var result = JsonSerializer.Deserialize<BulkUpdateRequest>(json);
        
        // Assert
        result.Should().BeEquivalentTo(sut);
    }

    [Fact]
    public void ShouldHandleDefault()
    {
        // Arrange
        const string json = "{ \"Ids\": [1, 2 ]}";

        // Act
        var result = JsonSerializer.Deserialize<BulkUpdateRequest>(json);

        // Assert
        result.Should().BeEquivalentTo(new BulkUpdateRequest
        {
            Ids = new[] { 1, 2 }
        });
    }
}

public record BulkUpdateRequest
{
    public IEnumerable<int> Ids { get; set; } = new List<int>();

    public IUpdateRequest Update { get; set; } = new UpdateEmptyRequest();
}
