using System.Text;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;

namespace TeachersTimeTable.Infrastructure.FileReaders;

public static class PdfTextReader
{
    public static string Read(Stream stream)
    {
        var sb = new StringBuilder();

        using var document = PdfDocument.Open(stream);

        foreach (Page page in document.GetPages())
        {
            sb.AppendLine(page.Text);
        }

        return sb.ToString();
    }
}
