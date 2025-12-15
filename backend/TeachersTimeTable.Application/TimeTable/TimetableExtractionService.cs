using TeachersTimeTable.Application.Ai;
using TeachersTimeTable.Application.Ocr;

namespace TeachersTimeTable.Application.Timetable;

public sealed class TimetableExtractionService
{
    private readonly IOcrService _ocrService;
    private readonly IAiClient _aiClient;

    public TimetableExtractionService(
        IOcrService ocrService,
        IAiClient aiClient)
    {
        _ocrService = ocrService;
        _aiClient = aiClient;
    }

    public async Task<TimetableExtractionResult> ExtractAsync(
        Stream fileStream,
        string fileName,
        CancellationToken cancellationToken)
    {
        // 1️⃣ OCR
        var ocrResult = await _ocrService.ExtractAsync(
            fileStream,
            fileName,
            cancellationToken);

        if (string.IsNullOrWhiteSpace(ocrResult.RawText))
        {
            throw new InvalidOperationException(
                "OCR did not return any readable text.");
        }

        // 2️⃣ Build Gemini prompt
        var prompt = GeminiPromptBuilder
            .BuildTimetableExtractionPrompt(ocrResult.RawText);

        // 3️⃣ Call Gemini
        var aiResponse = await _aiClient.GenerateAsync(
            prompt,
            cancellationToken);

        if (string.IsNullOrWhiteSpace(aiResponse))
        {
            throw new InvalidOperationException(
                "AI did not return any response.");
        }

        // 4️⃣ Return raw JSON (validation comes later)
        return new TimetableExtractionResult
        {
            RawJson = aiResponse
        };
    }
}
