using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Drawing;
using TeachersTimeTable.Application.Ocr;
using Tesseract;

namespace TeachersTimeTable.Infrastructure.Ocr;

public sealed class TesseractOcrService : IOcrService
{
    private readonly TesseractOptions _options;

    public TesseractOcrService(IOptions<TesseractOptions> options)
    {
        _options = options.Value;
    }

    public async Task<OcrResult> ExtractAsync(
        Stream stream,
        string fileName,
        CancellationToken cancellationToken)
    {
        if (stream == null || stream.Length == 0)
            throw new ArgumentException("Invalid file stream", nameof(stream));

        var extension = Path.GetExtension(fileName).ToLowerInvariant();

        if (extension is not ".png" and not ".jpg" and not ".jpeg")
            throw new NotSupportedException(
                $"File type '{extension}' is not supported for OCR.");

        // Load image
        using var bitmap = new Bitmap(stream);

        // Initialize Tesseract
        using var engine = new TesseractEngine(
            _options.TessDataPath,
            _options.Language,
            EngineMode.Default);

        using var pix = PixConverter.ToPix(bitmap);
        using var page = engine.Process(pix);

        var text = page.GetText();

        return new OcrResult
        {
            RawText = text?.Trim() ?? string.Empty
        };
    }
}
