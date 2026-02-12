using AppServices;

namespace AppServicesTests;

public class TravelFileParserTests
{
    [Fact]
    public void ParseCommute_NotImplemented_Throws()
    {
        var parser = new TravelFileParser();

        Assert.Throws<NotImplementedException>(() => parser.ParseCommute("Departure: 2026-02-06T14:30:00Z"));
    }

    [Fact]
    public void CommuteParseException_ContainsExpectedMessage()
    {
        var exception = new CommuteParseException(CommuteParseError.InvalidTravelMethod);

        Assert.Equal(CommuteParseError.InvalidTravelMethod, exception.ErrorCode);
        Assert.Contains("CAR or PUBLIC", exception.Message);
    }
}
