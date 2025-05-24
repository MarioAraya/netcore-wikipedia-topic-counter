using System.Net;
using Moq;
using Moq.Protected;
using Newtonsoft.Json.Linq;
using WikipediaTopicCounter.Services;
using Xunit;

namespace WikipediaTopicCounter.Tests;

public class WikipediaServiceTests
{
    private readonly Mock<HttpMessageHandler> _handlerMock;
    private readonly HttpClient _httpClient;
    private readonly WikipediaService _service;

    public WikipediaServiceTests()
    {
        _handlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_handlerMock.Object)
        {
            BaseAddress = new Uri("https://en.wikipedia.org/w/api.php")
        };
        _service = new WikipediaService(_httpClient);
    }

    [Fact]
    public async Task GetArticleContentAsync_ValidTitle_ReturnsContent()
    {
        // Arrange
        var title = "Test_Article";
        var expectedContent = "This is the content of the test article.";

        var responseJson = new JObject
        {
            ["query"] = new JObject
            {
                ["pages"] = new JObject
                {
                    ["12345"] = new JObject
                    {
                        ["pageid"] = 12345,
                        ["extract"] = expectedContent
                    }
                }
            }
        };

        _handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseJson.ToString())
            });

        // Act
        var result = await _service.GetArticleContentAsync(title);

        // Assert
        Assert.Equal(expectedContent, result);
    }

    [Fact]
    public async Task GetArticleContentAsync_PageNotFound_ReturnsEmptyString()
    {
        // Arrange
        var title = "NonExistent_Article";

        var responseJson = new JObject
        {
            ["query"] = new JObject
            {
                ["pages"] = new JObject
                {
                    ["-1"] = new JObject
                    {
                        ["missing"] = ""
                    }
                }
            }
        };

        _handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseJson.ToString())
            });

        // Act
        var result = await _service.GetArticleContentAsync(title);

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public async Task GetArticleContentAsync_ApiError_ThrowsException()
    {
        // Arrange
        var title = "Error_Article";

        var responseJson = new JObject
        {
            ["error"] = new JObject
            {
                ["code"] = "invalidtitle",
                ["info"] = "The article title is invalid"
            }
        };

        _handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseJson.ToString())
            });

        // Act & Assert
        // var exception = await Xunit.Assert.ThrowsAsync<Exception>(() => _service.GetArticleContentAsync(title));
        try
        {
            await _service.GetArticleContentAsync(title);
            Assert.True(false, "Se esperaba que se lanzara una excepción");
        }
        catch (Exception ex)
        {
            Assert.Contains("Error de la API de Wikipedia", ex.Message);
        }
    }
}