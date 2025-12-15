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
            throw new InvalidOperationException(
                "OCR returned no readable text.");

        // 2️⃣ Prompt
        var prompt = GeminiPromptBuilder
            .BuildTimetableExtractionPrompt(ocrResult.RawText);

        // 3️⃣ AI call
        var aiResponse = await _aiClient.GenerateAsync(
            prompt,
            cancellationToken);

        if (string.IsNullOrWhiteSpace(aiResponse))
            throw new InvalidOperationException(
                "AI returned an empty response.");

        // 4️⃣ Extract & validate JSON
        var json = AiJsonExtractor.ExtractJsonArray(aiResponse);

        var entries = AiJsonExtractor
            .DeserializeArray<TimetableEntryDto>(json);

        // 5️⃣ Return typed result
        return new TimetableExtractionResult
        {
            Entries = entries
        };
    }
}
