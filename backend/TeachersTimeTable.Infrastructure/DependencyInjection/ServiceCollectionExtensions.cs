using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TeachersTimeTable.Application.Ai;
using TeachersTimeTable.Infrastructure.Ai;

namespace TeachersTimeTable.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGeminiAi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IAiClient>(sp =>
        {
            // HttpClient is provided by API layer
            var httpClient = sp.GetRequiredService<HttpClient>();

            var apiKey = configuration["Gemini:ApiKey"]
                ?? throw new InvalidOperationException(
                    "Gemini API key not configured");

            return new GeminiAiClient(httpClient, apiKey);
        });

        return services;
    }
}
