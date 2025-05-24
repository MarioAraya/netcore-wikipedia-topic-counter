using WikipediaTopicCounter.Services;
using Xunit;

namespace WikipediaTopicCounter.Tests;

public class TopicCounterServiceTests
{
    private readonly TopicCounterService _service;

    public TopicCounterServiceTests()
    {
        _service = new TopicCounterService();
    }

    [Fact]
    public void CountTopicOccurrences_EmptyContent_ReturnsZero()
    {
        // Arrange
        string content = "";
        string topic = "test";

        // Act
        var result = _service.CountTopicOccurrences(content, topic);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CountTopicOccurrences_EmptyTopic_ReturnsZero()
    {
        // Arrange
        string content = "This is a test content";
        string topic = "";

        // Act
        var result = _service.CountTopicOccurrences(content, topic);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CountTopicOccurrences_TopicNotInContent_ReturnsZero()
    {
        // Arrange
        string content = "This is a test content";
        string topic = "apple";

        // Act
        var result = _service.CountTopicOccurrences(content, topic);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void CountTopicOccurrences_TopicInContent_ReturnsCorrectCount()
    {
        // Arrange
        string content = "This test is a test for testing the test functionality";
        string topic = "test";

        // Act
        var result = _service.CountTopicOccurrences(content, topic);

        // Assert
        Assert.Equal(3, result); // "test" aparece 3 veces como palabra completa
    }

    [Fact]
    public void CountTopicOccurrences_IgnoresCase_ReturnsCorrectCount()
    {
        // Arrange
        string content = "Test is different from test but both are TEST";
        string topic = "test";

        // Act
        var result = _service.CountTopicOccurrences(content, topic);

        // Assert
        Assert.Equal(3, result); // "Test", "test" y "TEST" deben contarse
    }

    [Fact]
    public void GetTopicStatistics_ValidInput_ReturnsCorrectStatistics()
    {
        // Arrange
        string content = "Test is a test for testing the test framework. Testing is important.";
        string topic = "test";

        // Act
        var result = _service.GetTopicStatistics(content, topic);

        // Assert
        Assert.Equal(3, result["Ocurrencias exactas"]); // "Test", "test", "test"
        Assert.Equal(5, result["Ocurrencias parciales"]); // Incluye "testing" y "Testing"
        Assert.Equal(12, result["Total de palabras"]); // Total de palabras en el contenido
    }
}