namespace TeachersTimeTable.Infrastructure.Ai;

public sealed class GeminiOptions
{
    public string ApiKey { get; init; } = string.Empty;
    public string Model { get; init; } = "models/gemini-2.5-flash";
}

