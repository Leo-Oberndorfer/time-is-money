using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AppServices;

public partial class ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : DbContext(options)
{
    public DbSet<CommuteEntity> Commutes => Set<CommuteEntity>();
    public DbSet<CarCommuteDataEntity> CarCommuteData => Set<CarCommuteDataEntity>();
    public DbSet<PublicCommuteDataEntity> PublicCommuteData => Set<PublicCommuteDataEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommuteEntity>(entity =>
        {
            entity.ToTable("Commutes");
            entity.Property(e => e.ChosenTravel).HasConversion<string>();
            entity.Property(e => e.Verdict).HasConversion<string>();
            entity.Property(e => e.EurPerMinutePerPerson).HasColumnType("REAL");

            entity.HasOne(e => e.CarData)
                .WithOne(c => c.Commute)
                .HasForeignKey<CarCommuteDataEntity>(c => c.CommuteId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.PublicData)
                .WithOne(p => p.Commute)
                .HasForeignKey<PublicCommuteDataEntity>(p => p.CommuteId)
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
