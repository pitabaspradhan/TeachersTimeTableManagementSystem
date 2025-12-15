using TeachersTimeTable.Application.Ai;
using TeachersTimeTable.Application.Ocr;

namespace TeachersTimeTable.Application.Services;

public interface ITimetableExtractionService
{
    Task<IReadOnlyList<TimetableRowAiResult>> ExtractAsync(
        OcrResult ocrResult,
        CancellationToken cancellationToken);
}
