using Tesseract;

namespace TeachersTimeTable.Infrastructure.FileReaders;

public static class ImageOcrReader
{
    public static string Read(Stream stream, string tessDataPath)
    {
        using var engine = new TesseractEngine(
            tessDataPath, "eng", EngineMode.Default);

        using var ms = new MemoryStream();
        stream.CopyTo(ms);

        using var pix = Pix.LoadFromMemory(ms.ToArray());
        using var page = engine.Process(pix);

        return page.GetText();
    }
}
