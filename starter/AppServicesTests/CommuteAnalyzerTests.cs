using AppServices;

namespace AppServicesTests;

public class CommuteAnalysisServiceTests
{
    [Fact]
    public void Analyze_NotImplemented_Throws()
    {
        var service = new CommuteAnalysisService();
        var commute = new Commute(
            DepartureUtc: DateTimeOffset.UtcNow,
            Destination: "Office",
            ChosenTravelMethod: CommuteTravelMethod.Car,
            ScheduledArrivalUtc: null,
            Car: new CarCommuteData(10m, 20, 5.0m, 1.5m, 1),
            Public: new PublicCommuteData(30, false));

        Assert.Throws<NotImplementedException>(() => service.Analyze(commute));
    }
}
