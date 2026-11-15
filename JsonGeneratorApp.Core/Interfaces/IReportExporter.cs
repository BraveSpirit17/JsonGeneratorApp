using JsonGeneratorApp.Core.Models;

namespace JsonGeneratorApp.Core.Interfaces;

public interface IReportExporter
{
    string FileExtension { get; }
    string FilterDescription { get; }
    void Export(List<Record> records, string filePath);
}
