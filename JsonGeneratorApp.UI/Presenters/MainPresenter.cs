using JsonGeneratorApp.Core.Exporters;
using JsonGeneratorApp.Core.Interfaces;
using JsonGeneratorApp.Core.Models;
using JsonGeneratorApp.Core.Services;
using JsonGeneratorApp.UI.Views;

namespace JsonGeneratorApp.UI.Presenters;

public class MainPresenter
{
    private readonly IMainView _view;
    private readonly Dictionary<string, IReportExporter> _exporters;

    private FileWatcher _fileWatcher;
    private List<Record> _currentRecords = [];

    public MainPresenter(IMainView view)
    {
        _view = view ?? throw new ArgumentNullException(nameof(view));

        _exporters = new Dictionary<string, IReportExporter>(StringComparer.OrdinalIgnoreCase)
        {
            { ".json", new JsonExporter() },
            { ".xlsx", new ExcelExporter() }
        };

        // Subscribe to view events
        _view.SelectDirectoryClicked += OnSelectDirectoryClicked;
        _view.ExportReportClicked += OnExportReportClicked;
        _view.ViewClosing += OnViewClosing;

        _view.CanExport = false;
    }

    private void OnSelectDirectoryClicked(object sender, EventArgs e)
    {
        string selectedPath = _view.PromptForDirectory();
        if (string.IsNullOrWhiteSpace(selectedPath))
            return;

        // Stop previous watcher and reset current data
        _fileWatcher?.Dispose();
        _currentRecords.Clear();
        _view.UpdateRecordsGrid(_currentRecords);
        _view.CanExport = false;

        _view.SelectedDirectory = selectedPath;

        // Start a new FileWatcher
        _fileWatcher = new FileWatcher(selectedPath);
        _fileWatcher.LogMessage += msg => _view.AppendLog(msg);
        _fileWatcher.RecordsMatched += OnRecordsMatched;
    }

    private void OnRecordsMatched(List<Record> records)
    {
        _currentRecords = records;
        _view.UpdateRecordsGrid(_currentRecords);
        _view.CanExport = _currentRecords.Any();
    }

    private void OnExportReportClicked(object sender, EventArgs e)
    {
        if (!_currentRecords.Any())
        {
            _view.ShowError("No data to export.");
            return;
        }

        string savePath = _view.PromptForSaveFilePath("report");
        if (string.IsNullOrWhiteSpace(savePath))
            return;

        string extension = Path.GetExtension(savePath);
        if (_exporters.TryGetValue(extension, out var exporter))
        {
            exporter.Export(_currentRecords, savePath);
            _view.AppendLog($"Report ({extension}) exported: {savePath}");
            _view.ShowInfo($"Report successfully saved:\n{savePath}", "Success");
        }
        else
        {
            _view.ShowError($"Unsupported file format: {extension}");
        }
    }

    private void OnViewClosing(object sender, EventArgs e)
    {
        _fileWatcher?.Dispose();
    }
}
