namespace AppServices;

public interface ICommuteDecisionService
{
    CommuteAnalysis Analyze(Commute commute);
}

public record CommuteAnalysis(
    DateTimeOffset CarArrivalUtc,
    DateTimeOffset PublicArrivalUtc,
    int CarPoints,
    int PublicPoints,
    DecisionVerdict Verdict,
    decimal? EurPerMinutePerPerson);

public class CommuteDecisionService : ICommuteDecisionService
{
    public CommuteAnalysis Analyze(Commute commute)
        => throw new NotImplementedException("Decision and reimbursement logic is part of the exercise.");
}
