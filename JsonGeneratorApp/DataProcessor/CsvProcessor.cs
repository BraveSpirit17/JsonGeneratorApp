using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using JsonGeneratorApp.Models;

namespace JsonGeneratorApp.DataProcessor
{
    public class CsvProcessor
    {
        public List<CsvData> ProcessCsvFile(string filePath)
        {
            
            // var files = Directory.GetFiles(filePath, "*.csv");
            
            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<CsvData>().ToList();
                    return records;
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, вывод в лог или уведомление пользователю
                Console.WriteLine($"Ошибка при обработке CSV файла: {ex.Message}");
                return null;
            }
        }
    }
}