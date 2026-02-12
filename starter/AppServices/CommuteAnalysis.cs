namespace AppServices;

public interface ICommuteAnalyzer
{
    CommuteAnalysisResult Analyze(CommuteDraft commute);
}

public record CommuteAnalysisResult(
    int CarPoints,
    int PublicPoints,
    DecisionVerdict Verdict,
    decimal? MoneyPerMinutePerPerson);

public class CommuteAnalyzer : ICommuteAnalyzer
{
    public CommuteAnalysisResult Analyze(CommuteDraft commute)
    {
        throw new NotImplementedException("Commute reimbursement/decision logic is intentionally left for the exercise.");
    }
}
