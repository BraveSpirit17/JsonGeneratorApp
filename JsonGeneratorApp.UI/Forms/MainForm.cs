using JsonGeneratorApp.Core.Models;
using JsonGeneratorApp.UI.Presenters;
using JsonGeneratorApp.UI.Views;

namespace JsonGeneratorApp.UI.Forms;

public partial class MainForm : Form, IMainView
{
    private readonly MainPresenter _presenter;

    public MainForm()
    {
        InitializeComponent();

        // Bind the form to the presenter
        _presenter = new MainPresenter(this);

        // Forward standard WinForms events to IMainView interface events
        btnSelectFolder.Click += (s, e) => SelectDirectoryClicked?.Invoke(this, EventArgs.Empty);
        btnExport.Click += (s, e) => ExportReportClicked?.Invoke(this, EventArgs.Empty);
        FormClosing += (s, e) => ViewClosing?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler SelectDirectoryClicked;
    public event EventHandler ExportReportClicked;
    public event EventHandler ViewClosing;

    string IMainView.SelectedDirectory
    {
        get => txtDirectory.Text;
        set => txtDirectory.Text = value;
    }

    bool IMainView.CanExport
    {
        set => ExecuteOnUiThread(() => btnExport.Enabled = value);
    }

    public void UpdateRecordsGrid(List<Record> records)
    {
        ExecuteOnUiThread(() =>
        {
            // Reset DataSource before updating so DataGridView redraws properly
            dgvRecords.DataSource = null;
            dgvRecords.DataSource = records;
        });
    }

    public void AppendLog(string message)
    {
        ExecuteOnUiThread(() =>
        {
            lstLogs.Items.Add(message);
            // Auto-scroll logs to the bottom
            lstLogs.TopIndex = lstLogs.Items.Count - 1;
        });
    }

    public void ShowInfo(string message, string title = "Information")
    {
        ExecuteOnUiThread(() => MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Information));
    }

    public void ShowError(string message, string title = "Error")
    {
        ExecuteOnUiThread(() => MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error));
    }

    public string PromptForDirectory()
    {
        using (var dialog = new FolderBrowserDialog())
        {
            dialog.Description = "Select a folder to watch for CSV and XML files";
            return dialog.ShowDialog(this) == DialogResult.OK ? dialog.SelectedPath : null;
        }
    }

    public string PromptForSaveFilePath(string defaultFileName)
    {
        using (var dialog = new SaveFileDialog())
        {
            dialog.Filter = "JSON files (*.json)|*.json|Excel spreadsheets (*.xlsx)|*.xlsx";
            dialog.FileName = defaultFileName;
            return dialog.ShowDialog(this) == DialogResult.OK ? dialog.FileName : null;
        }
    }

    // Helper method for safe invocation from FileWatcher background threads
    private void ExecuteOnUiThread(Action action)
    {
        if (IsDisposed) return;

        if (InvokeRequired)
            Invoke(action);
        else
            action();
    }
}