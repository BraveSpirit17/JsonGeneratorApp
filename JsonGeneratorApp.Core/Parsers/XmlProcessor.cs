using JsonGeneratorApp.Core.Models;
using System.Globalization;
using System.Xml.Linq;

namespace JsonGeneratorApp.Core.Parsers;

public class XmlProcessor
{
    public event Action<string> OnWarning;

    public List<Card> ProcessXmlFile(string filePath)
    {
        var validCards = new List<Card>();

        if (!File.Exists(filePath))
            return validCards;

        try
        {
            var xDocument = XDocument.Load(filePath);
            var cardElements = xDocument.Descendants("Card");

            foreach (var element in cardElements)
            {
                // Safe parsing for each field
                bool isUserIdValid = int.TryParse(element.Attribute("UserId")?.Value, out int userId);
                bool isPanValid = long.TryParse(element.Element("Pan")?.Value?.Replace(" ", ""), out long pan);
                bool isDateValid = DateTime.TryParse(element.Element("ExpDate")?.Value, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime expDate);

                if (!isUserIdValid || !isPanValid || !isDateValid || userId <= 0)
                {
                    OnWarning?.Invoke($"Skipped invalid <Card> record in file {Path.GetFileName(filePath)}");
                    continue;
                }

                validCards.Add(new Card
                {
                    UserId = userId,
                    Pan = pan,
                    ExpDate = expDate
                });
            }
        }
        catch (Exception ex)
        {
            OnWarning?.Invoke($"Error parsing XML ({Path.GetFileName(filePath)}): {ex.Message}");
        }

        return validCards;
    }
}