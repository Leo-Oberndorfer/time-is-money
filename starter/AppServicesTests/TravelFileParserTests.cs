using AppServices;

namespace AppServicesTests;

public class CommuteFileParserTests
{
    [Fact]
    public void ParseCommute_IsStarterStub_ThrowsNotImplemented()
    {
        var parser = new CommuteFileParser();

        var ex = Assert.Throws<NotImplementedException>(() => parser.ParseCommute("Departure: 2026-02-06T14:30:00Z"));
        Assert.Contains("exercise", ex.Message);
    }

    [Theory]
    [InlineData(CommuteParseError.EmptyFile, "empty")]
    [InlineData(CommuteParseError.InvalidSeparator, "=====")]
    [InlineData(CommuteParseError.InvalidTravelMethod, "CAR or PUBLIC")]
    public void CommuteParseException_ContainsExpectedMessage(CommuteParseError error, string expectedText)
    {
        var ex = new CommuteParseException(error);

        Assert.Equal(error, ex.ErrorCode);
        Assert.Contains(expectedText, ex.Message, StringComparison.OrdinalIgnoreCase);
    }
}
