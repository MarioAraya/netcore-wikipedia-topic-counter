using Microsoft.Extensions.DependencyInjection;
using WikipediaTopicCounter.Services;

namespace WikipediaTopicCounter;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Wikipedia Topic Counter - v0.4.0");
        Console.WriteLine("================================");

        // Configurar inyección de dependencias
        var services = new ServiceCollection();
        services.AddHttpClient<WikipediaService>();
        services.AddSingleton<TopicCounterService>();

        var serviceProvider = services.BuildServiceProvider();

        var wikipediaService = serviceProvider.GetRequiredService<WikipediaService>();
        var topicCounterService = serviceProvider.GetRequiredService<TopicCounterService>();

        // Hardcoded values for testing
        var title = "Artificial_intelligence";
        var topic = "machine learning";
        var content = await wikipediaService.GetArticleContentAsync(title);
        var statistics = topicCounterService.GetTopicStatistics(content, topic);

        Console.WriteLine("\nResultados:");
        Console.WriteLine($"- Artículo: {title}");
        Console.WriteLine($"- Tema buscado: {topic}");

        foreach (var stat in statistics)
        {
            Console.WriteLine($"- {stat.Key}: {stat.Value}");
        }
    }
}