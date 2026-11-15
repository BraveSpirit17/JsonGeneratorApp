using JsonGeneratorApp.Core.Models;

namespace JsonGeneratorApp.UI.Views;

public interface IMainView
{
    // Properties for reading and writing control states
    string SelectedDirectory { get; set; }
    bool CanExport { set; }

    // User action events
    event EventHandler SelectDirectoryClicked;
    event EventHandler ExportReportClicked;
    event EventHandler ViewClosing;

    // UI update methods
    void UpdateRecordsGrid(List<Record> records);
    void AppendLog(string message);
    void ShowInfo(string message, string title = "Information");
    void ShowError(string message, string title = "Error");

    // Dialog windows (moved to View so Presenter does not depend on WinForms)
    string PromptForDirectory();
    string PromptForSaveFilePath(string defaultFileName);
}
