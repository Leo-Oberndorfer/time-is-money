using AppServices;
using Microsoft.EntityFrameworkCore;
using TestInfrastructure;

namespace AppServicesTests;

public class DatabaseTestsWithClassFixture(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
{
    [Fact]
    public async Task CommuteEntity_Crud_WorksWithCarAndPublicChildData()
    {
        await using (var context = new ApplicationDataContext(fixture.Options))
        {
            await context.PublicCommuteData.ExecuteDeleteAsync();
            await context.CarCommuteData.ExecuteDeleteAsync();
            await context.Commutes.ExecuteDeleteAsync();
        }

        int id;
        await using (var context = new ApplicationDataContext(fixture.Options))
        {
            var commute = new CommuteEntity
            {
                DepartureUtc = new DateTimeOffset(2026, 02, 06, 14, 30, 0, TimeSpan.Zero),
                ScheduledArrivalUtc = new DateTimeOffset(2026, 02, 06, 15, 35, 0, TimeSpan.Zero),
                Destination = "Home",
                ChosenTravel = CommuteTravelMethod.Car,
                Verdict = DecisionVerdict.NoDifference,
                CarData = new CarCommuteDataEntity
                {
                    AdditionalPassengers = 1,
                    DistanceKm = 35,
                    DurationMinutes = 35,
                    AverageConsumptionLPer100Km = 5.1m,
                    FuelPricePerLiterEur = 1.54m
                },
                PublicData = new PublicCommuteDataEntity
                {
                    DurationMinutes = 65,
                    Delayed = false
                }
            };

            context.Commutes.Add(commute);
            await context.SaveChangesAsync();
            id = commute.Id;
        }

        await using (var context = new ApplicationDataContext(fixture.Options))
        {
            var loaded = await context.Commutes
                .Include(c => c.CarData)
                .Include(c => c.PublicData)
                .SingleAsync(c => c.Id == id);

            Assert.Equal("Home", loaded.Destination);
            Assert.Equal(CommuteTravelMethod.Car, loaded.ChosenTravel);
            Assert.Equal(35, loaded.CarData.DurationMinutes);
            Assert.Equal(65, loaded.PublicData.DurationMinutes);
        }
    }
}
