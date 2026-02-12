using AppServices;

namespace AppServicesTests;

public class ReimbursementCalculatorTests
{
    [Fact]
    public void Analyze_WhenCalled_ThrowsNotImplemented()
    {
        var analyzer = new CommuteAnalyzer();
        var draft = new CommuteDraft(
            DepartureUtc: new DateTimeOffset(2026, 2, 6, 14, 30, 0, TimeSpan.Zero),
            ScheduledArrivalUtc: new DateTimeOffset(2026, 2, 6, 15, 35, 0, TimeSpan.Zero),
            Destination: "Home",
            ChosenTravel: CommuteMethod.Car,
            Car: new CarCommuteDraft(1, 35m, 35, 5.1m, 1.54m),
            Public: new PublicCommuteDraft(65, false));

        var ex = Assert.Throws<NotImplementedException>(() => analyzer.Analyze(draft));

        Assert.Contains("intentionally", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void DecisionVerdict_ContainsExpectedValues()
    {
        Assert.Contains(DecisionVerdict.ChosenBetter, Enum.GetValues<DecisionVerdict>());
        Assert.Contains(DecisionVerdict.ChosenWorse, Enum.GetValues<DecisionVerdict>());
        Assert.Contains(DecisionVerdict.NoDifference, Enum.GetValues<DecisionVerdict>());
    }
}
