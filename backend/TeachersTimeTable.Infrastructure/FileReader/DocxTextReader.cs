using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using static System.Net.Mime.MediaTypeNames;

namespace TeachersTimeTable.Infrastructure.FileReaders;

public static class DocxTextReader
{
    public static string Read(Stream stream)
    {
        using var document = WordprocessingDocument.Open(stream, false);

        return string.Join(
            Environment.NewLine,
            document.MainDocumentPart!
                .Document
                .Body!
                .Descendants<DocumentFormat.OpenXml.Drawing.Text>()
                .Select(t => t.Text));
    }
}
