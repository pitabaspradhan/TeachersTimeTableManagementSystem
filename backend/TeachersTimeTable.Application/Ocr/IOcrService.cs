namespace TeachersTimeTable.Application.Ocr;

public interface IOcrService
{
    Task<OcrResult> ExtractAsync(
        Stream fileStream,
        string fileName,
        CancellationToken cancellationToken);
}
