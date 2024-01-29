using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using JsonGeneratorApp.Models;

namespace JsonGeneratorApp.DataProcessor
{
    public class XmlProcessor
    {
        public List<Card> ProcessXmlFile(string filePath)
        {
            try
            {
                var xDocument = XDocument.Load(filePath);
                var records = xDocument.Descendants("Card")
                    .Select(r => new Card
                    {
                        UserId = int.Parse(r.Attribute("UserId")?.Value ?? "0"),
                        Pan = long.Parse(r.Element("Pan")?.Value ?? "0"),
                        ExpDate = DateTime.Parse(r.Element("ExpDate")?.Value ?? "0")
                    })
                    .ToList();
                
                return records;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке XML файла: {ex.Message}");

                return null;
            }
        }
    }
}