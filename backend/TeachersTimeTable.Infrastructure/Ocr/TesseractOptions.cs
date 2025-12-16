namespace TeachersTimeTable.Infrastructure.Ocr;

public sealed class TesseractOptions
{
    public string TessDataPath { get; init; } = string.Empty;
    public string Language { get; init; } = "eng";
}