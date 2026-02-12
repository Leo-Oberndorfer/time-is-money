using AppServices;
using Microsoft.EntityFrameworkCore;
using TestInfrastructure;

namespace AppServicesTests;

public class DatabaseTestsWithClassFixture(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
{
    [Fact]
    public async Task CommuteEntity_Crud_Works()
    {
        await using (var context = new ApplicationDataContext(fixture.Options))
        {
            context.Commutes.RemoveRange(context.Commutes);
            await context.SaveChangesAsync();
        }

        var departureUtc = new DateTimeOffset(2026, 02, 06, 14, 30, 0, TimeSpan.Zero);

        int id;
        await using (var context = new ApplicationDataContext(fixture.Options))
        {
            var commute = new CommuteEntity
            {
                DepartureUtc = departureUtc,
                Destination = "Home",
                ChosenTravelMethod = CommuteTravelMethod.Car,
                ScheduledArrivalUtc = departureUtc.AddMinutes(65),
                CarDistanceKm = 35m,
                CarDurationMinutes = 35,
                CarAverageConsumptionLPer100Km = 5.1m,
                CarSpentEur = 1.54m,
                CarAdditionalPassengers = 1,
                PublicDurationMinutes = 65,
                PublicDelayed = false
            };

            context.Commutes.Add(commute);
            await context.SaveChangesAsync();
            id = commute.Id;
        }

        await using (var context = new ApplicationDataContext(fixture.Options))
        {
            var loaded = await context.Commutes.SingleAsync(c => c.Id == id);
            Assert.Equal("Home", loaded.Destination);
            Assert.Equal(CommuteTravelMethod.Car, loaded.ChosenTravelMethod);
            Assert.Equal(35m, loaded.CarDistanceKm, 3);
        }
    }
}
