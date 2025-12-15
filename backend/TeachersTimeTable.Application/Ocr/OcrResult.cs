namespace TeachersTimeTable.Application.Ocr;

public sealed record OcrResult
{
    public string RawText { get; init; } = string.Empty;
}
