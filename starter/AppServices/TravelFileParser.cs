namespace AppServices;

public interface ICommuteFileParser
{
    CommuteDraft Parse(string textContent);
}

public record CommuteDraft(
    DateTimeOffset DepartureUtc,
    DateTimeOffset? ScheduledArrivalUtc,
    string Destination,
    CommuteMethod ChosenTravel,
    CarCommuteDraft Car,
    PublicCommuteDraft Public);

public record CarCommuteDraft(
    int AdditionalPassengers,
    decimal DistanceKm,
    int DurationMinutes,
    decimal AverageConsumptionLPer100Km,
    decimal FuelPricePerLiterEur);

public record PublicCommuteDraft(int DurationMinutes, bool Delayed);

public enum CommuteParseError
{
    EmptyFile,
    InvalidSeparator,
    MissingHeaderBlock,
    MissingCarBlock,
    MissingPublicBlock,
    MissingRequiredField,
    InvalidDateFormat,
    ScheduledArrivalBeforeDeparture,
    InvalidMethod,
    EmptyDestination,
    InvalidNumber,
    InvalidBoolean,
    UnknownField
}

public class CommuteParseException(CommuteParseError errorCode, string? details = null)
    : Exception($"{GetMessage(errorCode)}{(string.IsNullOrWhiteSpace(details) ? string.Empty : $": {details}")}")
{
    private static string GetMessage(CommuteParseError errorCode) => errorCode switch
    {
        CommuteParseError.EmptyFile => "The commute file is empty",
        CommuteParseError.InvalidSeparator => "Expected separator line '====='",
        CommuteParseError.MissingHeaderBlock => "Header block is missing",
        CommuteParseError.MissingCarBlock => "CAR block is missing",
        CommuteParseError.MissingPublicBlock => "PUBLIC block is missing",
        CommuteParseError.MissingRequiredField => "A required field is missing",
        CommuteParseError.InvalidDateFormat => "A date field is invalid",
        CommuteParseError.ScheduledArrivalBeforeDeparture => "Scheduled arrival must be after departure",
        CommuteParseError.InvalidMethod => "Method must be CAR or PUBLIC",
        CommuteParseError.EmptyDestination => "Destination must not be empty",
        CommuteParseError.InvalidNumber => "Numeric value is invalid",
        CommuteParseError.InvalidBoolean => "Boolean value is invalid",
        CommuteParseError.UnknownField => "Unknown field in file",
        _ => "Unknown parse error"
    };

    public CommuteParseError ErrorCode { get; } = errorCode;
}

public class CommuteFileParser : ICommuteFileParser
{
    public CommuteDraft Parse(string textContent)
    {
        throw new NotImplementedException("Parser implementation is intentionally left for the exercise.");
    }
}
