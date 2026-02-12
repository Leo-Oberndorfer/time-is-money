using AppServices;
using Microsoft.EntityFrameworkCore;
using TestInfrastructure;

namespace AppServicesTests;

public class DatabaseTestsWithClassFixture(DatabaseFixture fixture) : IClassFixture<DatabaseFixture>
{
    [Fact]
    public async Task CommuteEntity_CanBeStoredWithCarAndPublicData()
    {
        await using (var cleanup = new ApplicationDataContext(fixture.Options))
        {
            cleanup.Commutes.RemoveRange(cleanup.Commutes);
            await cleanup.SaveChangesAsync();
        }

        int commuteId;
        await using (var context = new ApplicationDataContext(fixture.Options))
        {
            var entity = new CommuteEntity
            {
                DepartureUtc = new DateTimeOffset(2026, 02, 06, 14, 30, 0, TimeSpan.Zero),
                ScheduledArrivalUtc = new DateTimeOffset(2026, 02, 06, 15, 35, 0, TimeSpan.Zero),
                Destination = "Home",
                ChosenTravel = CommuteMethod.Car,
                Car = new CarCommuteDataEntity
                {
                    AdditionalPassengers = 1,
                    DistanceKm = 35m,
                    DurationMinutes = 35,
                    AverageConsumptionLPer100Km = 5.1m,
                    FuelPricePerLiterEur = 1.54m
                },
                Public = new PublicCommuteDataEntity
                {
                    DurationMinutes = 65,
                    Delayed = false
                }
            };

            context.Commutes.Add(entity);
            await context.SaveChangesAsync();
            commuteId = entity.Id;
        }

        await using (var context = new ApplicationDataContext(fixture.Options))
        {
            var saved = await context.Commutes
                .Include(c => c.Car)
                .Include(c => c.Public)
                .SingleAsync(c => c.Id == commuteId);

            Assert.Equal("Home", saved.Destination);
            Assert.Equal(CommuteMethod.Car, saved.ChosenTravel);
            Assert.Equal(35, saved.Car.DurationMinutes);
            Assert.Equal(65, saved.Public.DurationMinutes);
        }
    }
}
