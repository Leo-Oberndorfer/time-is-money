namespace AppServices;

public enum CommuteTravelMethod
{
    Car = 1,
    Public = 2
}

public enum DecisionVerdict
{
    ChosenBetter = 1,
    ChosenWorse = 2,
    NoDifference = 3
}

public class CommuteEntity
{
    public int Id { get; set; }

    public DateTimeOffset DepartureUtc { get; set; }
    public DateTimeOffset? ScheduledArrivalUtc { get; set; }
    public string Destination { get; set; } = string.Empty;
    public CommuteTravelMethod ChosenTravel { get; set; }

    public CarCommuteDataEntity CarData { get; set; } = new();
    public PublicCommuteDataEntity PublicData { get; set; } = new();

    // Calculated fields (persisted once the logic is implemented).
    public int CarPoints { get; set; }
    public int PublicPoints { get; set; }
    public DecisionVerdict Verdict { get; set; } = DecisionVerdict.NoDifference;
    public decimal? EurPerMinutePerPerson { get; set; }
}

public class CarCommuteDataEntity
{
    public int Id { get; set; }
    public int CommuteId { get; set; }
    public CommuteEntity? Commute { get; set; }

    public int AdditionalPassengers { get; set; }
    public decimal DistanceKm { get; set; }
    public int DurationMinutes { get; set; }
    public decimal AverageConsumptionLPer100Km { get; set; }
    public decimal FuelPricePerLiterEur { get; set; }
}

public class PublicCommuteDataEntity
{
    public int Id { get; set; }
    public int CommuteId { get; set; }
    public CommuteEntity? Commute { get; set; }

    public int DurationMinutes { get; set; }
    public bool Delayed { get; set; }
}
