using AppServices;

namespace WebApi;

public static class TravelEndpoints
{
    public static IEndpointRouteBuilder MapTravelEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/commutes")
            .WithTags("Commutes");

        group.MapPost("/import", ImportCommuteFile)
            .Accepts<IFormFile>("multipart/form-data")
            .DisableAntiforgery()
            .Produces<CommuteDetailsDto>(StatusCodes.Status201Created)
            .Produces<CommuteUploadErrorDto>(StatusCodes.Status400BadRequest);

        group.MapGet("/", GetCommutes)
            .Produces<List<CommuteListItemDto>>(StatusCodes.Status200OK);

        group.MapGet("/{id:int}", GetCommuteById)
            .Produces<CommuteDetailsDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/statistics", GetStatistics)
            .Produces<CommuteStatisticsDto>(StatusCodes.Status200OK);

        return app;
    }

    private static Task<IResult> ImportCommuteFile(
        IFormFile file,
        ApplicationDataContext db,
        ITravelFileParser parser,
        ICommuteAnalysisService analysisService)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> GetCommutes(ApplicationDataContext db)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> GetCommuteById(int id, ApplicationDataContext db)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> GetStatistics(ApplicationDataContext db)
    {
        throw new NotImplementedException();
    }
}

public record CommuteListItemDto(
    int Id,
    DateTimeOffset DepartureUtc,
    string Destination,
    string ChosenTravelMethod,
    int CarDurationMinutes,
    int PublicDurationMinutes,
    string? DecisionVerdict,
    decimal? EurPerMinutePerPerson);

public record CommuteDetailsDto(
    int Id,
    DateTimeOffset DepartureUtc,
    string Destination,
    string ChosenTravelMethod,
    DateTimeOffset? ScheduledArrivalUtc,
    CarCommuteDto Car,
    PublicCommuteDto Public,
    CommuteAnalysisDto? Analysis);

public record CarCommuteDto(
    decimal DistanceKm,
    int DurationMinutes,
    decimal AverageConsumptionLPer100Km,
    decimal SpentEur,
    int? AdditionalPassengers);

public record PublicCommuteDto(int DurationMinutes, bool Delayed);

public record CommuteAnalysisDto(
    DateTimeOffset CarArrivalUtc,
    DateTimeOffset PublicArrivalUtc,
    int CarPoints,
    int PublicPoints,
    string DecisionVerdict,
    decimal? EurPerMinutePerPerson);

public record CommuteStatisticsDto(
    int CountCarChosen,
    int CountPublicChosen,
    decimal AvgDurationCar,
    decimal AvgDurationPublic,
    decimal? AvgFuelCostCarChosen,
    decimal TotalSpentEur,
    decimal TotalSavedEur,
    decimal TotalKmCarChosen,
    decimal TotalFuelLitersCarChosen,
    decimal TotalCo2Grams,
    int TotalDelaysPublic,
    int GoodDecisionsCount,
    int BadDecisionsCount);

public record CommuteUploadErrorDto(string ErrorCode, string Message);
