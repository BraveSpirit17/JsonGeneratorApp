using Xunit;

namespace JsonGeneratorApp.Tests.MatcherTests;

public class RecordSecurityTests
{
    [Theory]
    [InlineData(4111222233334444, "411122******4444")]
    [InlineData(5500001234567890, "550000******7890")]
    [InlineData(12345, "*****")]
    public void MaskPan_ValidAndShortNumbers_ReturnsExpectedMask(long inputPan, string expectedMask)
    {
        // Act
        string actual = Core.Models.Record.MaskPan(inputPan);

        // Assert
        Assert.Equal(expectedMask, actual);
    }
}