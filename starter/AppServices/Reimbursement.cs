namespace AppServices;

public interface ICommuteAnalysisService
{
    CommuteAnalysisResult Analyze(Commute commute);
}

public record CommuteAnalysisResult(
    CommuteDecisionVerdict Verdict,
    decimal? EurPerMinutePerPerson,
    int CarPoints,
    int PublicPoints);

public class CommuteAnalysisService : ICommuteAnalysisService
{
    public CommuteAnalysisResult Analyze(Commute commute)
    {
        throw new NotImplementedException();
    }
}
