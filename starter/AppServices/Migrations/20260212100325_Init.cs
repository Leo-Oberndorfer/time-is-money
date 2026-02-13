using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppServices.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Commutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DepartureUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    ScheduledArrivalUtc = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    Destination = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    ChosenTravelMethod = table.Column<int>(type: "INTEGER", nullable: false),
                    CarDistanceKm = table.Column<double>(type: "REAL", nullable: false),
                    CarDurationMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    CarAverageConsumptionLPer100Km = table.Column<double>(type: "REAL", nullable: false),
                    CarSpentEur = table.Column<double>(type: "REAL", nullable: false),
                    CarAdditionalPassengers = table.Column<int>(type: "INTEGER", nullable: true),
                    PublicDurationMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    PublicDelayed = table.Column<bool>(type: "INTEGER", nullable: false),
                    DecisionVerdict = table.Column<int>(type: "INTEGER", nullable: true),
                    EurPerMinutePerPerson = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commutes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commutes");
        }
    }
}
