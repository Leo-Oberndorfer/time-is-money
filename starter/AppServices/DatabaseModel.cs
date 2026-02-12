namespace AppServices;

public class CommuteEntity
{
    public int Id { get; set; }

    public DateTimeOffset DepartureUtc { get; set; }
    public DateTimeOffset? ScheduledArrivalUtc { get; set; }
    public string Destination { get; set; } = string.Empty;
    public CommuteTravelMethod ChosenTravelMethod { get; set; }

    public decimal CarDistanceKm { get; set; }
    public int CarDurationMinutes { get; set; }
    public decimal CarAverageConsumptionLPer100Km { get; set; }
    public decimal CarSpentEur { get; set; }
    public int? CarAdditionalPassengers { get; set; }

    public int PublicDurationMinutes { get; set; }
    public bool PublicDelayed { get; set; }

    // Persisted analysis values; to be computed by application logic in later exercises.
    public CommuteDecisionVerdict? DecisionVerdict { get; set; }
    public decimal? EurPerMinutePerPerson { get; set; }
}

public enum CommuteTravelMethod
{
    Car = 1,
    Public = 2
}

public enum CommuteDecisionVerdict
{
    ChosenBetter = 1,
    ChosenWorse = 2,
    NoDifference = 3
}
