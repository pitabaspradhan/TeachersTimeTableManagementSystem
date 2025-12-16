using TeachersTimeTable.Application.Ai;
using TeachersTimeTable.Application.Ocr;
using TeachersTimeTable.Application.Services;

namespace TeachersTimeTable.Application.TimetableExtraction;

public sealed class TimetableExtractionService
    : ITimetableExtractionService
{
    private readonly IAiClient _aiClient;

    public TimetableExtractionService(IAiClient aiClient)
    {
        _aiClient = aiClient;
    }

    public async Task<IReadOnlyList<TimetableRowAiResult>> ExtractAsync(
        OcrResult ocrResult,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(ocrResult.RawText))
            throw new InvalidOperationException(
                "OCR returned no readable text.");

        var prompt = GeminiPromptBuilder
            .BuildTimetableExtractionPrompt(ocrResult.RawText);

        var aiResponse = await _aiClient.GenerateAsync(
            prompt,
            cancellationToken);

        if (string.IsNullOrWhiteSpace(aiResponse))
            throw new InvalidOperationException(
                "AI returned an empty response.");

        var json = AiJsonExtractor.ExtractJsonArray(aiResponse);

        return AiJsonExtractor
            .DeserializeArray<TimetableRowAiResult>(json.ToJsonString());
    }
}
