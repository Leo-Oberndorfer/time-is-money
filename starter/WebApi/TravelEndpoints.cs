using AppServices;

namespace WebApi;

public static class CommuteEndpoints
{
    public static IEndpointRouteBuilder MapCommuteEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/commutes")
            .WithTags("Commutes");

        group.MapPost("/upload", UploadCommuteFile)
            .Accepts<IFormFile>("multipart/form-data")
            .DisableAntiforgery()
            .Produces<CommuteDetailsDto>(StatusCodes.Status201Created)
            .Produces<CommuteUploadErrorDto>(StatusCodes.Status400BadRequest)
            .WithDescription("Uploads a commute text file and creates a commute entry.");

        group.MapGet("/", ListCommutes)
            .Produces<List<CommuteListItemDto>>(StatusCodes.Status200OK)
            .WithDescription("Lists imported commutes (newest first).");

        group.MapGet("/{id:int}", GetCommuteById)
            .Produces<CommuteDetailsDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithDescription("Gets one commute including computed values.");

        group.MapGet("/statistics", GetStatistics)
            .Produces<CommuteStatisticsDto>(StatusCodes.Status200OK)
            .WithDescription("Returns aggregated commute statistics.");

        return app;
    }

    private static Task<IResult> UploadCommuteFile(
        IFormFile file,
        ApplicationDataContext db,
        ICommuteFileParser parser,
        ICommuteDecisionService decisionService)
        => throw new NotImplementedException("Endpoint implementation is part of the exercise.");

    private static Task<IResult> ListCommutes(ApplicationDataContext db)
        => throw new NotImplementedException("Endpoint implementation is part of the exercise.");

    private static Task<IResult> GetCommuteById(int id, ApplicationDataContext db)
        => throw new NotImplementedException("Endpoint implementation is part of the exercise.");

    private static Task<IResult> GetStatistics(ApplicationDataContext db)
        => throw new NotImplementedException("Endpoint implementation is part of the exercise.");
}

public record CommuteListItemDto(
    int Id,
    DateTimeOffset DepartureUtc,
    string Destination,
    CommuteTravelMethod ChosenTravel,
    int CarDurationMinutes,
    int PublicDurationMinutes,
    DecisionVerdict Verdict,
    decimal? EurPerMinutePerPerson);

public record CommuteDetailsDto(
    int Id,
    DateTimeOffset DepartureUtc,
    DateTimeOffset? ScheduledArrivalUtc,
    string Destination,
    CommuteTravelMethod ChosenTravel,
    CarCommuteDataDto Car,
    PublicCommuteDataDto Public,
    DateTimeOffset CarArrivalUtc,
    DateTimeOffset PublicArrivalUtc,
    int CarPoints,
    int PublicPoints,
    DecisionVerdict Verdict,
    decimal? EurPerMinutePerPerson);

public record CarCommuteDataDto(
    int AdditionalPassengers,
    decimal DistanceKm,
    int DurationMinutes,
    decimal AverageConsumptionLPer100Km,
    decimal FuelPricePerLiterEur);

public record PublicCommuteDataDto(int DurationMinutes, bool Delayed);

public record CommuteStatisticsDto(
    int CountCarChosen,
    int CountPublicChosen,
    double AvgDurationCar,
    double AvgDurationPublic,
    decimal? AvgFuelCostCarChosen,
    decimal TotalSpentEur,
    decimal TotalSavedEur,
    decimal TotalKmCarChosen,
    decimal TotalFuelLitersCarChosen,
    decimal TotalCo2Grams,
    int GoodDecisionsCount,
    int BadDecisionsCount,
    int TotalDelaysPublic);

public record CommuteUploadErrorDto(string ErrorCode, string Message);
