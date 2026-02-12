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
            .Produces<CommuteUploadErrorDto>(StatusCodes.Status400BadRequest);

        group.MapGet("/", ListCommutes)
            .Produces<List<CommuteListItemDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id:int}", GetCommuteById)
            .Produces<CommuteDetailsDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/statistics", GetStatistics)
            .Produces<CommuteStatisticsDto>(StatusCodes.Status200OK);

        return app;
    }

    private static IResult UploadCommuteFile(
        IFormFile file,
        ICommuteFileParser parser,
        ICommuteAnalyzer analyzer,
        ApplicationDataContext db)
    {
        throw new NotImplementedException("Upload workflow is intentionally not implemented in starter code.");
    }

    private static IResult ListCommutes(ApplicationDataContext db)
    {
        throw new NotImplementedException("List endpoint logic is intentionally not implemented in starter code.");
    }

    private static IResult GetCommuteById(int id, ApplicationDataContext db)
    {
        throw new NotImplementedException("Detail endpoint logic is intentionally not implemented in starter code.");
    }

    private static IResult GetStatistics(ApplicationDataContext db)
    {
        throw new NotImplementedException("Statistics endpoint logic is intentionally not implemented in starter code.");
    }
}

public record CommuteListItemDto(
    int Id,
    DateTimeOffset DepartureUtc,
    string Destination,
    CommuteMethod ChosenTravel,
    int CarDurationMinutes,
    int PublicDurationMinutes,
    DecisionVerdict? Verdict,
    decimal? MoneyPerMinutePerPerson);

public record CommuteDetailsDto(
    int Id,
    DateTimeOffset DepartureUtc,
    DateTimeOffset? ScheduledArrivalUtc,
    string Destination,
    CommuteMethod ChosenTravel,
    CarCommuteDataDto Car,
    PublicCommuteDataDto Public,
    int? CarPoints,
    int? PublicPoints,
    DecisionVerdict? Verdict,
    decimal? MoneyPerMinutePerPerson);

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
    decimal? AvgDurationCar,
    decimal? AvgDurationPublic,
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
