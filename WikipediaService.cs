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
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("El título del artículo no puede estar vacío.", nameof(title));
        }

        try
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

            // Verificar si hay errores en la respuesta
            if (json["error"] != null)
            {
                throw new Exception($"Error de la API de Wikipedia: {json["error"]["info"]}");
            }

            var pages = json["query"]["pages"].Children<JProperty>();
            var page = pages.First().Value;

            // Verificar si existe la página
            if (page["pageid"] == null || page["missing"] != null)
            {
                return string.Empty; // Página no encontrada
            }

            return page["extract"]?.ToString() ?? string.Empty;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception($"Error al conectar con la API de Wikipedia: {ex.Message}", ex);
        }
        catch (Exception ex) when (!(ex is ArgumentException))
        {
            throw new Exception($"Error al procesar la respuesta de Wikipedia: {ex.Message}", ex);
        }
    }
}