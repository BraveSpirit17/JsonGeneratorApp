using JsonGeneratorApp.Core.Models;
using JsonGeneratorApp.Core.Parsers;
using Newtonsoft.Json;

namespace JsonGeneratorApp.Core.Services;

public class FileWatcher : IDisposable
{
    private readonly FileSystemWatcher _watcher;
    private readonly CsvProcessor _csvProcessor;
    private readonly XmlProcessor _xmlProcessor;

    private readonly List<CsvData> _csvDatas = [];
    private readonly List<Card> _cards = [];
    private readonly object _syncLock = new();

    public event Action<List<Record>> RecordsMatched;
    public event Action<string> LogMessage;

    public FileWatcher(string folderPath)
    {
        _csvProcessor = new CsvProcessor();
        _xmlProcessor = new XmlProcessor();

        DeleteOldReport();
        ScanExistingFiles(folderPath);

        _watcher = new FileSystemWatcher
        {
            Path = folderPath,
            Filter = "*.*",
            EnableRaisingEvents = true,
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.CreationTime | NotifyFilters.LastWrite
        };

        _watcher.Created += OnFileCreated;
        Log($"Started watching folder: {folderPath}");
    }

    private void ScanExistingFiles(string folderPath)
    {
        var files = Directory.GetFiles(folderPath)
            .Where(f => f.EndsWith(".csv", StringComparison.OrdinalIgnoreCase) ||
                        f.EndsWith(".xml", StringComparison.OrdinalIgnoreCase));

        foreach (var file in files)
        {
            Task.Run(() => ProcessFileWithRetryAsync(file));
        }
    }

    private void OnFileCreated(object sender, FileSystemEventArgs e)
    {
        string ext = Path.GetExtension(e.FullPath);
        if (!ext.Equals(".csv", StringComparison.OrdinalIgnoreCase) &&
            !ext.Equals(".xml", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        Task.Run(() => ProcessFileWithRetryAsync(e.FullPath));
    }

    private async Task ProcessFileWithRetryAsync(string filePath)
    {
        const int maxRetries = 5;
        const int delayMs = 300;

        for (int attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                // Check if the file is locked by another process
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    // File is readable, close the test stream
                }

                ParseAndMatch(filePath);
                return;
            }
            catch (IOException)
            {
                if (attempt == maxRetries)
                {
                    Log($"Error: file {Path.GetFileName(filePath)} is locked and cannot be read.");
                    return;
                }
                await Task.Delay(delayMs);
            }
            catch (Exception ex)
            {
                Log($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
                return;
            }
        }
    }

    private void ParseAndMatch(string filePath)
    {
        string extension = Path.GetExtension(filePath);
        string fileName = Path.GetFileName(filePath);

        lock (_syncLock)
        {
            if (extension.Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                var data = _csvProcessor.ProcessCsvFile(filePath);
                if (data != null && data.Any())
                {
                    _csvDatas.AddRange(data);
                    Log($"Loaded CSV ({fileName}): {data.Count} records.");
                }
            }
            else if (extension.Equals(".xml", StringComparison.OrdinalIgnoreCase))
            {
                var cards = _xmlProcessor.ProcessXmlFile(filePath);
                if (cards != null && cards.Any())
                {
                    _cards.AddRange(cards);
                    Log($"Loaded XML ({fileName}): {cards.Count} cards.");
                }
            }

            MatchRecords();
        }
    }

    private void MatchRecords()
    {
        if (!_csvDatas.Any() || !_cards.Any())
            return;

        var result = _csvDatas
            .Join(_cards, csv => csv.UserId, card => card.UserId, (csv, card) => new Record
            {
                UserId = card.UserId,
                ExpDate = card.ExpDate,
                MaskedPan = Record.MaskPan(card.Pan),
                FirstName = csv.Name,
                LastName = csv.SecondName,
                Phone = csv.Number
            })
            .GroupBy(r => new { r.UserId }) // Prevent duplicates on re-read
            .Select(g => g.First())
            .ToList();

        if (result.Any())
        {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.json");
            var json = JsonConvert.SerializeObject(new Root { Records = result }, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(jsonPath, json);

            Log($"Matched records: {result.Count}. Intermediate JSON updated.");
            RecordsMatched?.Invoke(result);
        }
    }

    private void DeleteOldReport()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.json");
        if (File.Exists(filePath))
        {
            try { File.Delete(filePath); }
            catch (Exception ex) { Log($"Failed to delete old data.json: {ex.Message}"); }
        }
    }

    private void Log(string message)
    {
        LogMessage?.Invoke($"[{DateTime.Now:HH:mm:ss}] {message}");
    }

    public void Dispose()
    {
        if (_watcher != null)
        {
            _watcher.EnableRaisingEvents = false;
            _watcher.Created -= OnFileCreated;
            _watcher.Dispose();
            Log("Folder watching stopped.");
        }
    }
}
