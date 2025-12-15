using System.Text;
using System.Text.Json;
using TeachersTimeTable.Application.Ai;

namespace TeachersTimeTable.Infrastructure.Ai;

public sealed class GeminiAiClient : IAiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GeminiAiClient(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    public async Task<string> GenerateAsync(
        string prompt,
        CancellationToken cancellationToken)
    {
        var requestUri =
            $"https://generativelanguage.googleapis.com/v1/models/gemini-1.5-pro:generateContent?key={_apiKey}";

        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
        };

        var json = JsonSerializer.Serialize(requestBody);

        using var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        using var response = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new InvalidOperationException(
                $"Gemini API call failed: {response.StatusCode} - {error}");
        }

        using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

        using var document = await JsonDocument.ParseAsync(stream, cancellationToken: cancellationToken);

        // Extract text response from Gemini
        var text = document
            .RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

        if (string.IsNullOrWhiteSpace(text))
            throw new InvalidOperationException("Gemini returned empty response");

        return text;
    }
}
