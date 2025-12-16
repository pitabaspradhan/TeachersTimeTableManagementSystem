using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using TeachersTimeTable.Application.Ai;

namespace TeachersTimeTable.Infrastructure.Ai;

public sealed class GeminiAiClient : IAiClient
{
    private readonly HttpClient _httpClient;
    private readonly GeminiOptions _options;

    public GeminiAiClient(
        HttpClient httpClient,
        IOptions<GeminiOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;

        if (string.IsNullOrWhiteSpace(_options.ApiKey))
            throw new InvalidOperationException("Gemini API key is missing");
    }

    public async Task<string> GenerateAsync(
    string prompt,
    CancellationToken cancellationToken)
    {
        var requestUri =
    $"https://generativelanguage.googleapis.com/v1/{_options.Model}:generateContent?key={_options.ApiKey}";

        var request = new
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

        var response = await _httpClient.PostAsJsonAsync(
            requestUri,
            request,
            cancellationToken);

        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"Gemini API failed ({response.StatusCode}): {body}");
        }

        return body;
    }
}
