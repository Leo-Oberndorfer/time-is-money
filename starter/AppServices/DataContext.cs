using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AppServices;

public partial class ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : DbContext(options)
{
    public DbSet<CommuteEntity> Commutes => Set<CommuteEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommuteEntity>(entity =>
        {
            entity.ToTable("Commutes");
            entity.Property(e => e.Destination).HasMaxLength(200);
            entity.Property(e => e.ChosenTravel).HasConversion<string>();
            entity.Property(e => e.Verdict).HasConversion<string>();
            entity.Property(e => e.MoneyPerMinutePerPerson).HasColumnType("REAL");

            entity.HasOne(e => e.Car)
                .WithOne(e => e.Commute)
                .HasForeignKey<CarCommuteDataEntity>(e => e.CommuteId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Public)
                .WithOne(e => e.Commute)
                .HasForeignKey<PublicCommuteDataEntity>(e => e.CommuteId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CarCommuteDataEntity>(entity =>
        {
            entity.ToTable("CommuteCarData");
            entity.Property(e => e.DistanceKm).HasColumnType("REAL");
            entity.Property(e => e.AverageConsumptionLPer100Km).HasColumnType("REAL");
            entity.Property(e => e.FuelPricePerLiterEur).HasColumnType("REAL");
        });

        modelBuilder.Entity<PublicCommuteDataEntity>(entity =>
        {
            entity.ToTable("CommutePublicData");
        });
    }
}

public class ApplicationDataContextFactory : IDesignTimeDbContextFactory<ApplicationDataContext>
{
    public ApplicationDataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDataContext>();

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .Build();

        var path = configuration["Database:path"] ?? throw new InvalidOperationException("Database path not configured.");
        var fileName = configuration["Database:fileName"] ?? throw new InvalidOperationException("Database file name not configured.");
        optionsBuilder.UseSqlite($"Data Source={path}/{fileName}");

        return new ApplicationDataContext(optionsBuilder.Options);
    }
}
