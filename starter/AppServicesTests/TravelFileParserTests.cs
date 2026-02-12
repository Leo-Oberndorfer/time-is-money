using AppServices;

namespace AppServicesTests;

public class TravelFileParserTests
{
    [Fact]
    public void Parse_WhenCalled_ThrowsNotImplemented()
    {
        var parser = new CommuteFileParser();

        var ex = Assert.Throws<NotImplementedException>(() => parser.Parse("Departure: 2026-02-06T14:30:00Z"));

        Assert.Contains("intentionally", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void CommuteParseException_ContainsErrorCodeAndDetails()
    {
        var ex = new CommuteParseException(CommuteParseError.InvalidMethod, "Travel: BIKE");

        Assert.Equal(CommuteParseError.InvalidMethod, ex.ErrorCode);
        Assert.Contains("CAR or PUBLIC", ex.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("Travel: BIKE", ex.Message, StringComparison.Ordinal);
    }

    [Theory]
    [InlineData(CommuteParseError.EmptyFile)]
    [InlineData(CommuteParseError.InvalidSeparator)]
    [InlineData(CommuteParseError.MissingHeaderBlock)]
    [InlineData(CommuteParseError.MissingCarBlock)]
    [InlineData(CommuteParseError.MissingPublicBlock)]
    [InlineData(CommuteParseError.MissingRequiredField)]
    [InlineData(CommuteParseError.InvalidDateFormat)]
    [InlineData(CommuteParseError.ScheduledArrivalBeforeDeparture)]
    [InlineData(CommuteParseError.InvalidMethod)]
    [InlineData(CommuteParseError.EmptyDestination)]
    [InlineData(CommuteParseError.InvalidNumber)]
    [InlineData(CommuteParseError.InvalidBoolean)]
    [InlineData(CommuteParseError.UnknownField)]
    public void CommuteParseException_SupportsAllSpecificationErrors(CommuteParseError error)
    {
        var ex = new CommuteParseException(error);

        Assert.Equal(error, ex.ErrorCode);
        Assert.False(string.IsNullOrWhiteSpace(ex.Message));
    }
}
