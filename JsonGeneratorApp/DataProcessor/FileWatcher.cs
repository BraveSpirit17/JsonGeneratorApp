using JsonGeneratorApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace JsonGeneratorApp.DataProcessor
{
    public class FileWatcher
    {
        private readonly FileSystemWatcher _csvWatcher;
        private readonly FileSystemWatcher _xmlWatcher;

        private List<CsvData> _csvDatas = new List<CsvData>();
        private List<Card> _cards = new List<Card>();

        public FileWatcher(string folderPath)
        {
            DeleteFile();

            _csvWatcher = new FileSystemWatcher
            {
                Path = folderPath,
                Filter = "*.csv",
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.CreationTime
            };

            _xmlWatcher = new FileSystemWatcher
            {
                Path = folderPath,
                Filter = "*.xml",
                EnableRaisingEvents = true,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.CreationTime
            };

            _csvWatcher.Created += OnFileCreated;
            _xmlWatcher.Created += OnFileCreated;
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e)
        {
            HandleNewFile(e.FullPath);
        }

        private void HandleNewFile(string filePath)
        {
            if (Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                var csvProcessor = new CsvProcessor();

                _csvDatas.AddRange(csvProcessor.ProcessCsvFile(filePath));
            }
            else if (Path.GetExtension(filePath).Equals(".xml", StringComparison.OrdinalIgnoreCase))
            {
                var xmlProcessor = new XmlProcessor();

                _cards.AddRange(xmlProcessor.ProcessXmlFile(filePath));
            }

            if (_csvDatas.Any() && _cards.Any())
            {
                var result = _csvDatas
                    .Join(_cards, csv => csv.UserId, card => card.UserId, (cvs, card) => new Record
                    {
                        UserId = card.UserId,
                        ExpDate = card.ExpDate,
                        Pan = card.Pan,
                        FirstName = cvs.Name,
                        LastName = cvs.SecondName,
                        Phone = cvs.Number
                    })
                    .ToList();

                if (result.Any())
                {
                    MessageBox.Show("Есть связанные файлы для создания отчета.",
                        "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var json = JsonConvert.SerializeObject(new Root { Records = result });

                    File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.json"), json);
                }
            }
        }

        private void DeleteFile()
        {
            string filePathToDelete = Path.Combine(Application.StartupPath, "data.json");

            try
            {
                if (File.Exists(filePathToDelete))
                {
                    File.Delete(filePathToDelete);
                }
            }
            finally
            {

            }
        }
    }
}