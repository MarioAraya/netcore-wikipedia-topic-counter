using System.Text.RegularExpressions;

namespace WikipediaTopicCounter.Services;

public class TopicCounterService
{
    public int CountTopicOccurrences(string content, string topic)
    {
        if (string.IsNullOrWhiteSpace(content) || string.IsNullOrWhiteSpace(topic))
        {
            return 0;
        }

        // Ignorar mayúsculas/minúsculas y contar palabras completas
        var pattern = $@"\b{Regex.Escape(topic)}\b";
        var regex = new Regex(pattern, RegexOptions.IgnoreCase);

        return regex.Matches(content).Count;
    }

    public Dictionary<string, int> GetTopicStatistics(string content, string topic)
    {
        var result = new Dictionary<string, int>();

        if (string.IsNullOrWhiteSpace(content) || string.IsNullOrWhiteSpace(topic))
        {
            return result;
        }

        // Contar ocurrencias exactas
        result["Ocurrencias exactas"] = CountTopicOccurrences(content, topic);

        // Contar ocurrencias parciales (cuando el tema está dentro de otras palabras)
        var partialPattern = Regex.Escape(topic);
        var partialRegex = new Regex(partialPattern, RegexOptions.IgnoreCase);
        result["Ocurrencias parciales"] = partialRegex.Matches(content).Count;

        // Total de palabras en el artículo
        var wordPattern = @"\b\w+\b";
        var wordRegex = new Regex(wordPattern);
        result["Total de palabras"] = wordRegex.Matches(content).Count;

        return result;
    }
}