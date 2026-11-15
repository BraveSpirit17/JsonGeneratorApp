using CsvHelper;
using JsonGeneratorApp.Core.Models;
using System.Globalization;
using System.Text;

namespace JsonGeneratorApp.Core.Parsers;

public class CsvProcessor
{
    public event Action<string> OnWarning;

    public List<CsvData> ProcessCsvFile(string filePath)
    {
        if (!File.Exists(filePath))
            return [];

        try
        {
            // Automatically detect the delimiter (, or ;) from the first line
            string firstLine = File.ReadLines(filePath, Encoding.UTF8).FirstOrDefault() ?? "";
            char separator = firstLine.Contains(';') ? ';' : ',';

            var options = new CsvReaderOptions
            {
                Separator = separator,
                HasHeader = true,
                FormatProvider = CultureInfo.InvariantCulture,
                // Equivalent of BadDataFound in CsvHelper 34:
                OnMalformedRecord = args => OnWarning?.Invoke($"Malformed row in CSV (row {args.RecordNumber}): {args.RawRecord}")
            };

            using var reader = new StreamReader(filePath, Encoding.UTF8);
            using var csv = CsvReader.Create(reader, options);

            return [.. csv.GetRecords<CsvData>().Where(r => r.UserId > 0 && !string.IsNullOrWhiteSpace(r.Name))];
        }
        catch (Exception ex)
        {
            OnWarning?.Invoke($"Error reading CSV ({Path.GetFileName(filePath)}): {ex.Message}");
            return [];
        }
    }
}