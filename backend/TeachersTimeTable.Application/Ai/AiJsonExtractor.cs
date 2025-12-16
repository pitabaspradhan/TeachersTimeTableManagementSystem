using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;

namespace TeachersTimeTable.Application.Ai;

public static class AiJsonExtractor
{
    //public static string ExtractJsonArray(string aiResponse)
    //{
    //    if (string.IsNullOrWhiteSpace(aiResponse))
    //        throw new InvalidOperationException("AI response is empty.");

    //    // Extract first JSON array from response
    //    var match = Regex.Match(
    //        aiResponse,
    //        @"\[\s*{.*?}\s*\]",
    //        RegexOptions.Singleline);

    //    if (!match.Success)
    //        throw new InvalidOperationException(
    //            "AI response does not contain a valid JSON array.");

    //    return match.Value;
    //}

    //public static JsonArray ExtractJsonArray(string aiResponse)
    //{
    //    if (string.IsNullOrWhiteSpace(aiResponse))
    //        throw new InvalidOperationException("AI response is empty");

    //    JsonNode root;

    //    try
    //    {
    //        root = JsonNode.Parse(aiResponse)!;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new InvalidOperationException("AI response is not valid JSON", ex);
    //    }

    //    // CASE 1: Already a JSON array (rare but supported)
    //    if (root is JsonArray directArray)
    //        return directArray;

    //    // CASE 2: Gemini-style wrapper
    //    if (root is JsonArray wrapperArray &&
    //        wrapperArray.FirstOrDefault() is JsonObject first &&
    //        first["content"]?["parts"] is JsonArray parts &&
    //        parts.FirstOrDefault()?["text"]?.GetValue<string>() is string text)
    //    {
    //        text = text.Trim();

    //        // Remove markdown fences if any
    //        text = text.Replace("```json", "")
    //                   .Replace("```", "")
    //                   .Trim();

    //        var inner = JsonNode.Parse(text);

    //        if (inner is JsonArray innerArray)
    //            return innerArray;

    //        throw new InvalidOperationException("Gemini text does not contain a JSON array");
    //    }

    //    throw new InvalidOperationException("Unsupported AI response format");
    //}

    public static JsonArray ExtractJsonArray(string aiResponse)
    {
        if (string.IsNullOrWhiteSpace(aiResponse))
            throw new InvalidOperationException("AI response is empty");

        JsonNode root;
        try
        {
            root = JsonNode.Parse(aiResponse)!;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("AI response is not valid JSON", ex);
        }

        // 🔹 Case 1: { candidates: [ ... ] }
        if (root is JsonObject obj && obj["candidates"] is JsonArray candidates)
        {
            return ExtractFromCandidates(candidates);
        }

        // 🔹 Case 2: [ { content: { parts: [ { text } ] } } ]
        if (root is JsonArray array)
        {
            return ExtractFromCandidates(array);
        }

        throw new InvalidOperationException(
            $"Unsupported AI response format. Root type: {root.GetType().Name}");
    }

    private static JsonArray ExtractFromCandidates(JsonArray candidates)
    {
        var first = candidates.FirstOrDefault() as JsonObject
            ?? throw new InvalidOperationException("AI response array is empty");

        var text = first["content"]?["parts"]?[0]?["text"]?.GetValue<string>();

        if (string.IsNullOrWhiteSpace(text))
            throw new InvalidOperationException("AI response does not contain text");

        text = text.Replace("```json", "")
                   .Replace("```", "")
                   .Trim();

        JsonNode inner;
        try
        {
            inner = JsonNode.Parse(text)!;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("AI text is not valid JSON", ex);
        }

        if (inner is JsonArray timetable)
            return timetable;

        throw new InvalidOperationException("AI text does not contain a JSON array");
    }



    //public static JsonArray ExtractJsonArray(string raw)
    //{
    //    if (string.IsNullOrWhiteSpace(raw))
    //        throw new InvalidOperationException("Empty AI response");

    //    // 1. Remove markdown fences if present
    //    raw = raw.Replace("```json", "")
    //             .Replace("```", "")
    //             .Trim();

    //    JsonNode? node;

    //    try
    //    {
    //        node = JsonNode.Parse(raw);
    //    }
    //    catch
    //    {
    //        throw new InvalidOperationException("AI response is not valid JSON");
    //    }

    //    // 2. Root is array → OK
    //    if (node is JsonArray array)
    //        return array;

    //    // 3. Root is object → try to find array inside
    //    if (node is JsonObject obj)
    //    {
    //        foreach (var property in obj)
    //        {
    //            if (property.Value is JsonArray innerArray)
    //                return innerArray;
    //        }
    //    }

    //    throw new InvalidOperationException(
    //        $"AI response JSON does not contain an array. Actual root: {node.GetType().Name}");
    //}

    //public static IReadOnlyList<T> DeserializeArray<T>(string json)
    //{
    //    try
    //    {
    //        var result = JsonSerializer.Deserialize<List<T>>(
    //            json,
    //            new JsonSerializerOptions
    //            {
    //                PropertyNameCaseInsensitive = true
    //            });

    //        if (result == null || result.Count == 0)
    //            throw new InvalidOperationException(
    //                "Deserialized JSON is empty.");

    //        return result;
    //    }
    //    catch (JsonException ex)
    //    {
    //        throw new InvalidOperationException(
    //            "Failed to parse AI JSON response.", ex);
    //    }
    //}

    public static IReadOnlyList<T> DeserializeArray<T>(string json)
    {
        try
        {
            return JsonSerializer.Deserialize<List<T>>(
                       json,
                       new JsonSerializerOptions
                       {
                           PropertyNameCaseInsensitive = true,
                           AllowTrailingCommas = true
                       }) ?? [];
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                $"Failed to parse AI JSON response.\nJSON:\n{json}", ex);
        }
    }
}
