using Newtonsoft.Json.Linq;

namespace WikipediaTopicCounter.Services;

public class WikipediaService
{
    private readonly HttpClient _httpClient;

    public WikipediaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://en.wikipedia.org/w/api.php");
    }

    public async Task<string> GetArticleContentAsync(string title)
    {
        var parameters = new Dictionary<string, string>
        {
            { "action", "query" },
            { "format", "json" },
            { "titles", title },
            { "prop", "extracts" },
            { "explaintext", "true" } // Obtener solo texto plano sin formato HTML
        };

        string url = $"?{string.Join("&", parameters.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"))}";

        var response = await _httpClient.GetStringAsync(url);
        var json = JObject.Parse(response);

        var pages = json["query"]["pages"].Children<JProperty>();
        var page = pages.First().Value;

        return page["extract"]?.ToString() ?? string.Empty;
    }
}