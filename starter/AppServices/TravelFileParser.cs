namespace AppServices;

public interface ICommuteFileParser
{
    Commute ParseCommute(string content);
}

public record Commute(
    DateTimeOffset DepartureUtc,
    DateTimeOffset? ScheduledArrivalUtc,
    CommuteTravelMethod ChosenTravel,
    string Destination,
    CarCommuteData Car,
    PublicCommuteData Public);

public record CarCommuteData(
    int AdditionalPassengers,
    decimal DistanceKm,
    int DurationMinutes,
    decimal AverageConsumptionLPer100Km,
    decimal FuelPricePerLiterEur);

public record PublicCommuteData(int DurationMinutes, bool Delayed);

public enum CommuteParseError
{
    EmptyFile,
    MissingHeaderSection,
    MissingCarSection,
    MissingPublicSection,
    InvalidSeparator,
    MissingRequiredField,
    InvalidDepartureFormat,
    InvalidScheduledArrivalFormat,
    ScheduledArrivalBeforeOrEqualDeparture,
    InvalidTravelMethod,
    EmptyDestination,
    InvalidMethodBlock,
    InvalidNumericValue,
    InvalidBooleanValue,
    UnknownMethod,
    MultipleCommutesNotAllowed
}

public class CommuteParseException(CommuteParseError errorCode)
    : Exception(ErrorMessages.TryGetValue(errorCode, out var message) ? message : "Unknown parsing error.")
{
    private static readonly Dictionary<CommuteParseError, string> ErrorMessages = new()
    {
        { CommuteParseError.EmptyFile, "The commute file is empty." },
        { CommuteParseError.MissingHeaderSection, "The header section is missing." },
        { CommuteParseError.MissingCarSection, "The CAR section is missing." },
        { CommuteParseError.MissingPublicSection, "The PUBLIC section is missing." },
        { CommuteParseError.InvalidSeparator, "Sections must be separated by an exact '=====' line." },
        { CommuteParseError.MissingRequiredField, "A required field is missing." },
        { CommuteParseError.InvalidDepartureFormat, "Departure is not a valid ISO-8601 UTC timestamp." },
        { CommuteParseError.InvalidScheduledArrivalFormat, "Scheduled arrival is not a valid ISO-8601 UTC timestamp." },
        { CommuteParseError.ScheduledArrivalBeforeOrEqualDeparture, "Scheduled arrival must be after departure." },
        { CommuteParseError.InvalidTravelMethod, "Travel must be exactly CAR or PUBLIC." },
        { CommuteParseError.EmptyDestination, "Destination must not be empty." },
        { CommuteParseError.InvalidMethodBlock, "Method block content is invalid." },
        { CommuteParseError.InvalidNumericValue, "One of the numeric values is invalid." },
        { CommuteParseError.InvalidBooleanValue, "Boolean values must be true or false." },
        { CommuteParseError.UnknownMethod, "Unknown method encountered. Only CAR and PUBLIC are allowed." },
        { CommuteParseError.MultipleCommutesNotAllowed, "A commute file may contain exactly one commute." }
    };

    public CommuteParseError ErrorCode { get; } = errorCode;
}

public class CommuteFileParser : ICommuteFileParser
{
    public Commute ParseCommute(string content)
        => throw new NotImplementedException("Parser implementation is part of the exercise.");
}
