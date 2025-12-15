using TeachersTimeTable.Application.Ocr;
using TeachersTimeTable.Infrastructure.FileReaders;

namespace TeachersTimeTable.Infrastructure.Ocr;

public sealed class TesseractOcrService : IOcrService
{
    private readonly string _tessDataPath;

    public TesseractOcrService(string tessDataPath)
    {
        _tessDataPath = tessDataPath;
    }

    public Task<OcrResult> ExtractAsync(
        Stream fileStream,
        string fileName,
        CancellationToken cancellationToken)
    {
        var extension = Path.GetExtension(fileName).ToLowerInvariant();

        var text = extension switch
        {
            ".png" or ".jpg" or ".jpeg"
                => ImageOcrReader.Read(fileStream, _tessDataPath),

            ".pdf"
                => PdfTextReader.Read(fileStream),

            ".docx"
                => DocxTextReader.Read(fileStream),

            _ => throw new NotSupportedException(
                $"File type '{extension}' is not supported")
        };

        return Task.FromResult(new OcrResult { RawText = text });
    }
}
