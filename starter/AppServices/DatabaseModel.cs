namespace AppServices;

public enum CommuteMethod
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
    public CommuteMethod ChosenTravel { get; set; }

    public CarCommuteDataEntity Car { get; set; } = new();
    public PublicCommuteDataEntity Public { get; set; } = new();

    // Calculated values are intentionally persisted as optional placeholders.
    public int? CarPoints { get; set; }
    public int? PublicPoints { get; set; }
    public DecisionVerdict? Verdict { get; set; }
    public decimal? MoneyPerMinutePerPerson { get; set; }
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
