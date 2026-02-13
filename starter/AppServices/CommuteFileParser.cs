namespace AppServices;

public interface ITravelFileParser
{
    Commute ParseCommute(string textContent);
}

public record Commute(
    DateTimeOffset DepartureUtc,
    string Destination,
    CommuteTravelMethod ChosenTravelMethod,
    DateTimeOffset? ScheduledArrivalUtc,
    CarCommuteData Car,
    PublicCommuteData Public);

public record CarCommuteData(
    decimal DistanceKm,
    int DurationMinutes,
    decimal AverageConsumptionLPer100Km,
    decimal SpentEur,
    int? AdditionalPassengers);

public record PublicCommuteData(
    int DurationMinutes,
    bool Delayed);

public enum CommuteParseError
{
    EmptyFile,
    InvalidSectionSeparator,
    MissingHeaderSection,
    MissingCarSection,
    MissingPublicSection,
    InvalidDepartureDate,
    InvalidScheduledArrivalDate,
    ScheduledArrivalBeforeDeparture,
    EmptyDestination,
    InvalidTravelMethod,
    InvalidCarMethod,
    InvalidPublicMethod,
    MissingRequiredField,
    InvalidNumericValue,
    InvalidBooleanValue,
    MultipleCommutesInFile
}

public class CommuteParseException(CommuteParseError errorCode)
    : Exception(ErrorMessages.TryGetValue(errorCode, out var message) ? message : "Unknown parsing error.")
{
    private static readonly Dictionary<CommuteParseError, string> ErrorMessages = new()
    {
        { CommuteParseError.EmptyFile, "The commute file is empty." },
        { CommuteParseError.InvalidSectionSeparator, "Section separator must be exactly '====='." },
        { CommuteParseError.MissingHeaderSection, "Missing header section." },
        { CommuteParseError.MissingCarSection, "Missing CAR section." },
        { CommuteParseError.MissingPublicSection, "Missing PUBLIC section." },
        { CommuteParseError.InvalidDepartureDate, "Departure must be a valid ISO-8601 UTC timestamp." },
        { CommuteParseError.InvalidScheduledArrivalDate, "Scheduled arrival must be a valid ISO-8601 UTC timestamp." },
        { CommuteParseError.ScheduledArrivalBeforeDeparture, "Scheduled arrival must be after departure." },
        { CommuteParseError.EmptyDestination, "Destination must not be empty." },
        { CommuteParseError.InvalidTravelMethod, "Travel must be either CAR or PUBLIC." },
        { CommuteParseError.InvalidCarMethod, "CAR block must contain Method: CAR." },
        { CommuteParseError.InvalidPublicMethod, "PUBLIC block must contain Method: PUBLIC." },
        { CommuteParseError.MissingRequiredField, "A required field is missing." },
        { CommuteParseError.InvalidNumericValue, "A numeric value is invalid or not positive." },
        { CommuteParseError.InvalidBooleanValue, "Boolean values must be true or false." },
        { CommuteParseError.MultipleCommutesInFile, "A file can only contain one commute." }
    };

    public CommuteParseError ErrorCode { get; } = errorCode;
}

public class CommuteFileParser : ITravelFileParser
{
    public Commute ParseCommute(string textContent)
    {
        throw new NotImplementedException();
    }
}
