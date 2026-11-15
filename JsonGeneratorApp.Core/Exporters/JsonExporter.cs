using JsonGeneratorApp.Core.Interfaces;
using JsonGeneratorApp.Core.Models;
using Newtonsoft.Json;

namespace JsonGeneratorApp.Core.Exporters;

public class JsonExporter : IReportExporter
{
    public string FileExtension => ".json";
    public string FilterDescription => "JSON files (*.json)|*.json";

    public void Export(List<Record> records, string filePath)
    {
        var root = new Root { Records = records };
        var json = JsonConvert.SerializeObject(root, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }
}
