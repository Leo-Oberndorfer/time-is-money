using AppServices;

namespace AppServicesTests;

public class CommuteDecisionServiceTests
{
    [Fact]
    public void Analyze_IsStarterStub_ThrowsNotImplemented()
    {
        var service = new CommuteDecisionService();
        var commute = new Commute(
            DepartureUtc: new DateTimeOffset(2026, 2, 6, 14, 30, 0, TimeSpan.Zero),
            ScheduledArrivalUtc: null,
            ChosenTravel: CommuteTravelMethod.Car,
            Destination: "Home",
            Car: new CarCommuteData(1, 35m, 35, 5.1m, 1.54m),
            Public: new PublicCommuteData(65, false));

        Assert.Throws<NotImplementedException>(() => service.Analyze(commute));
    }
}
