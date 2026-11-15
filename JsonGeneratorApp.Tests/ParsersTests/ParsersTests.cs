using JsonGeneratorApp.Core.Parsers;
using Xunit;

namespace JsonGeneratorApp.Tests.ParsersTests;

public class ParsersTests
{
    [Fact]
    public void CsvProcessor_SemicolonDelimiter_ParsesCorrectly()
    {
        // Arrange
        string tempFile = Path.GetTempFileName();
        File.WriteAllText(tempFile, "userid;name;secondname;number\n101;;Simmons;+79991112233");
        var processor = new CsvProcessor();

        try
        {
            // Act
            var result = processor.ProcessCsvFile(tempFile);

            // Assert
            Assert.Single(result);
            Assert.Equal(101, result[0].UserId);
            Assert.Equal("Harvey", result[0].Name);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }

    [Fact]
    public void XmlProcessor_WithBrokenNode_SkipsInvalidAndReturnsValid()
    {
        // Arrange
        string tempFile = Path.GetTempFileName();
        string xmlContent = @"
                <Cards>
                    <Card UserId=""101"">
                        <Pan>4111222233334444</Pan>
                        <ExpDate>2028-12-31</ExpDate>
                    </Card>
                    <Card UserId=""invalid_id"">
                        <Pan>5555666677778888</Pan>
                        <ExpDate>2029-01-01</ExpDate>
                    </Card>
                </Cards>";
        File.WriteAllText(tempFile, xmlContent);
        var processor = new XmlProcessor();

        try
        {
            // Act
            var result = processor.ProcessXmlFile(tempFile);

            // Assert
            Assert.Single(result);
            Assert.Equal(101, result[0].UserId);
            Assert.Equal(4111222233334444, result[0].Pan);
        }
        finally
        {
            File.Delete(tempFile);
        }
    }
}
