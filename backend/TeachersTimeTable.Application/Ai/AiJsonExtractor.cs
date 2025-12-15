using System.Text.Json;
using System.Text.RegularExpressions;

namespace TeachersTimeTable.Application.Ai;

public static class AiJsonExtractor
{
    public static string ExtractJsonArray(string aiResponse)
    {
        if (string.IsNullOrWhiteSpace(aiResponse))
            throw new InvalidOperationException("AI response is empty.");

        // Extract first JSON array from response
        var match = Regex.Match(
            aiResponse,
            @"\[\s*{.*?}\s*\]",
            RegexOptions.Singleline);

        if (!match.Success)
            throw new InvalidOperationException(
                "AI response does not contain a valid JSON array.");

        return match.Value;
    }

    public static IReadOnlyList<T> DeserializeArray<T>(string json)
    {
        try
        {
            var result = JsonSerializer.Deserialize<List<T>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (result == null || result.Count == 0)
                throw new InvalidOperationException(
                    "Deserialized JSON is empty.");

            return result;
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                "Failed to parse AI JSON response.", ex);
        }
    }
}
