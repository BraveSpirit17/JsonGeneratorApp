using ClosedXML.Excel;
using JsonGeneratorApp.Core.Interfaces;
using JsonGeneratorApp.Core.Models;

namespace JsonGeneratorApp.Core.Exporters;

public class ExcelExporter : IReportExporter
{
    public string FileExtension => ".xlsx";
    public string FilterDescription => "Excel spreadsheets (*.xlsx)|*.xlsx";

    public void Export(List<Record> records, string filePath)
    {
        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.Worksheets.Add("Cards Report");

            // Headers
            worksheet.Cell(1, 1).Value = "User Id";
            worksheet.Cell(1, 2).Value = "First Name";
            worksheet.Cell(1, 3).Value = "Last Name";
            worksheet.Cell(1, 4).Value = "Phone";
            worksheet.Cell(1, 5).Value = "MaskedPan";
            worksheet.Cell(1, 6).Value = "Expiration Date";

            // Table header style
            var headerRange = worksheet.Range("A1:F1");
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;

            // Populate data
            for (int i = 0; i < records.Count; i++)
            {
                int row = i + 2;
                var record = records[i];

                worksheet.Cell(row, 1).Value = record.UserId;
                worksheet.Cell(row, 2).Value = record.FirstName;
                worksheet.Cell(row, 3).Value = record.LastName;
                worksheet.Cell(row, 4).Value = record.Phone;
                worksheet.Cell(row, 5).Value = record.MaskedPan;
                worksheet.Cell(row, 6).Value = record.ExpDate.ToString("MM/yyyy");
            }

            // Auto-fit column widths
            worksheet.Columns().AdjustToContents();
            workbook.SaveAs(filePath);
        }
    }
}
