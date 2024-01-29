using System;
using System.IO;
using System.Windows.Forms;
using JsonGeneratorApp.DataProcessor;

namespace JsonGeneratorApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Выберите директорию";
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;

                DialogResult result = folderBrowserDialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    directoryTextBox.Text = folderBrowserDialog.SelectedPath;

                    var fileWatcher = new FileWatcher(folderBrowserDialog.SelectedPath);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var jsonFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "report.json");

                var filePath = Path.Combine(Application.StartupPath, "data.json");
                var fileContent = File.ReadAllText(filePath);

                File.WriteAllText(jsonFilePath, fileContent);

                MessageBox.Show($"Отчет успешно создан. JSON файл сохранен по пути: {jsonFilePath}",
                                "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при создании отчета: {ex.Message}",
                                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}