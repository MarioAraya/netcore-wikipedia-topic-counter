using Microsoft.Extensions.DependencyInjection;
using WikipediaTopicCounter.Services;

namespace WikipediaTopicCounter;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("Wikipedia Topic Counter");
        Console.WriteLine("================================");

        // Configurar inyección de dependencias
        var services = new ServiceCollection();
        services.AddHttpClient<WikipediaService>();
        services.AddSingleton<TopicCounterService>();

        var serviceProvider = services.BuildServiceProvider();

        var wikipediaService = serviceProvider.GetRequiredService<WikipediaService>();
        var topicCounterService = serviceProvider.GetRequiredService<TopicCounterService>();

        Console.WriteLine("Este programa contará las ocurrencias de un tema en un artículo de Wikipedia.");
        Console.WriteLine("Para terminar la ejecución en cualquier momento, presiona Ctrl+C.");

        while (true)
        {
            try
            {
                // Solicitar título del artículo
                Console.WriteLine("\nIngrese el TÍTULO del artículo de Wikipedia (en inglés) (default: 'Artificial_intelligence'):");
                var title = Console.ReadLine()?.Trim() ?? "Artificial intelligence";
                title = string.IsNullOrWhiteSpace(title) ? "Artificial intelligence" : title;

                // Solicitar tema a buscar
                Console.WriteLine("Ingrese el TEMA a buscar en el artículo (default: 'AI'):");
                var topic = Console.ReadLine()?.Trim() ?? "Machine learning";
                topic = string.IsNullOrWhiteSpace(topic) ? "Machine learning" : topic;

                Console.WriteLine($"\nBuscando '{topic}' en el artículo '{title}'...");

                // Obtener contenido del artículo
                var content = await wikipediaService.GetArticleContentAsync(title);

                if (string.IsNullOrWhiteSpace(content))
                {
                    Console.WriteLine($"No se encontró el artículo '{title}' o está vacío.");
                    continue;
                }

                // Contar ocurrencias y mostrar estadísticas
                var statistics = topicCounterService.GetTopicStatistics(content, topic);

                Console.WriteLine("\nResultados:");
                Console.WriteLine($"- Artículo: {title}");
                Console.WriteLine($"- Tema buscado: {topic}");

                foreach (var stat in statistics)
                {
                    Console.WriteLine($"- {stat.Key}: {stat.Value}");
                }

                // Preguntar si desea continuar
                Console.WriteLine("\n¿Desea realizar otra búsqueda? (s/n)");
                var continuar = Console.ReadLine()?.Trim().ToLower();

                if (continuar != "s" && continuar != "si" && continuar != "sí")
                {
                    break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                Console.WriteLine("Inténtelo de nuevo.");
            }
        }

        Console.WriteLine("\n¡Gracias por usar Wikipedia Topic Counter!");
    }
}