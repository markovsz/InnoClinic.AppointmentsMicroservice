using Application.Abstractions;
using InnoClinic.SharedModels.DTOs.Appointments.Outgoing;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace Infrastructure.Services;

public class PdfGenerationService : IPdfGenerationService
{
    public byte[] Generate(ResultOutgoingDto outgoingDto)
    {


        // Must have write permissions to the path folder
        using Stream stream = new MemoryStream();
        PdfWriter writer = new PdfWriter(stream);
        PdfDocument pdf = new PdfDocument(writer);
        Document document = new Document(pdf);

        // Header
        Paragraph header = new Paragraph("InnoClinic")
           .SetTextAlignment(TextAlignment.CENTER)
           .SetFontSize(20);

        // New line
        Paragraph newline = new Paragraph(new Text("\n"));

        document.Add(newline);
        document.Add(header);

        // Add sub-header
        Paragraph subheader = new Paragraph("Result")
           .SetTextAlignment(TextAlignment.CENTER)
           .SetFontSize(15);
        document.Add(subheader);

        // Line separator
        LineSeparator ls = new LineSeparator(new SolidLine());
        document.Add(ls);

        // Add paragraph1
        Paragraph paragraph1 = new Paragraph("You results are ready");
        document.Add(paragraph1);

        // Table
        Table table = new Table(2, false);
        Cell cell11 = new Cell(1, 1)
           .SetBackgroundColor(ColorConstants.GRAY)
           .SetTextAlignment(TextAlignment.CENTER)
           .Add(new Paragraph("Name"));
        Cell cell12 = new Cell(1, 1)
           .SetBackgroundColor(ColorConstants.GRAY)
           .SetTextAlignment(TextAlignment.CENTER)
           .Add(new Paragraph("Description"));

        Cell cell21 = new Cell(1, 1)
           .SetTextAlignment(TextAlignment.CENTER)
           .Add(new Paragraph(nameof(outgoingDto.Complaints)));
        Cell cell22 = new Cell(1, 1)
           .SetTextAlignment(TextAlignment.CENTER)
           .Add(new Paragraph(outgoingDto.Complaints));

        Cell cell31 = new Cell(1, 1)
           .SetTextAlignment(TextAlignment.CENTER)
           .Add(new Paragraph(nameof(outgoingDto.Conclusion)));
        Cell cell32 = new Cell(1, 1)
           .SetTextAlignment(TextAlignment.CENTER)
           .Add(new Paragraph(outgoingDto.Conclusion));

        Cell cell41 = new Cell(1, 1)
           .SetTextAlignment(TextAlignment.CENTER)
           .Add(new Paragraph(nameof(outgoingDto.Recomendations)));
        Cell cell42 = new Cell(1, 1)
           .SetTextAlignment(TextAlignment.CENTER)
           .Add(new Paragraph(outgoingDto.Recomendations));

        table.AddCell(cell11);
        table.AddCell(cell12);
        table.AddCell(cell21);
        table.AddCell(cell22);
        table.AddCell(cell31);
        table.AddCell(cell32);
        table.AddCell(cell41);
        table.AddCell(cell42);

        document.Add(newline);
        document.Add(table);

        // Close document
        document.Close();

        MemoryStream memoryStream = null;
        if (stream is MemoryStream)
            memoryStream = stream as MemoryStream;
        var bytes = memoryStream?.ToArray();
        if (bytes is null)
            throw new Exception();

        return bytes;
    }
}
